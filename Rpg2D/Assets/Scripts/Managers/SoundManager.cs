using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager Instance => m_instance;

#if UNITY_EDITOR
    private Transform m_sfxParent;
    private Transform m_bgmParent;
#endif

    private uint m_bgmID = 0;
    private float m_musicVolume = 1;
    private GameObject m_bgmHolder;
    private Dictionary<uint, AudioSource> m_bgmDict = new Dictionary<uint, AudioSource>();

    private ulong m_sfxID = 0;
    private float m_sfxVolume = 1;
    private Dictionary<ulong, AudioSource> m_sfxDict = new Dictionary<ulong, AudioSource>();

    private Dictionary<AudioID, ulong> m_sharedSfxDict = new Dictionary<AudioID, ulong>();

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitialiseHierarchyWindow()
    {
#if UNITY_EDITOR
        m_sfxParent = new GameObject().transform;
        m_bgmParent = new GameObject().transform;
        m_sfxParent.name = "SfxGroup";
        m_bgmParent.name = "BgmGroup";
        DontDestroyOnLoad(m_sfxParent);
        DontDestroyOnLoad(m_bgmParent);
#endif
    }

    public void UpdateSfxVolume(float volume)
    {
        m_sfxVolume = volume;
        foreach (var sfx in m_sfxDict)
        {
            sfx.Value.volume = m_sfxVolume;
        }
    }

    public void UpdateMusicVolume(float volume)
    {
        m_musicVolume = volume;
        foreach (var bgm in m_bgmDict)
        {
            bgm.Value.volume = m_musicVolume;
        }
    }

    public void PlayBGM(AudioID audioID, ref uint id, bool isLoop = true)
    {
        if (m_bgmDict.ContainsKey(id))
        {
            m_bgmDict[id].loop = isLoop;
            m_bgmDict[id].Play();
            return;
        }

        if (m_bgmHolder == null)
        {
            m_bgmHolder = new GameObject();
            DontDestroyOnLoad(m_bgmHolder);
#if UNITY_EDITOR
            m_bgmHolder.transform.SetParent(m_bgmParent);
            DontDestroyOnLoad(m_bgmParent);
#endif
        }
#if UNITY_EDITOR
        m_bgmHolder.name = audioID.ToString();
#endif
        AudioSource audioSource = m_bgmHolder.AddComponent<AudioSource>();
        audioSource.clip = DataManager.Instance.LoadAudioData(audioID).audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = m_musicVolume;
        audioSource.Play();
        m_bgmID++;
        m_bgmDict.Add(m_bgmID, audioSource);
        id = m_bgmID;
    }

    public void PlaySfx(AudioID audioID, ref ulong id, bool isLoop = false)
    {
        if (m_sfxDict.ContainsKey(id))
        {
            m_sfxDict[id].loop = isLoop;
            m_sfxDict[id].volume = m_sfxVolume;
            m_sfxDict[id].Play();
            return;
        }

        GameObject sfxHolder = new GameObject();
#if UNITY_EDITOR
        sfxHolder.name = audioID.ToString();
        sfxHolder.transform.SetParent(m_sfxParent);
        DontDestroyOnLoad(m_sfxParent);
#endif
        DontDestroyOnLoad(sfxHolder);
        AudioSource audioSource = sfxHolder.AddComponent<AudioSource>();
        audioSource.clip = DataManager.Instance.LoadAudioData(audioID).audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = m_sfxVolume;
        audioSource.Play();
        m_sfxID++;
        m_sfxDict.Add(m_sfxID, audioSource);
        id = m_sfxID;
    }

    public void PlaySharedSfx(AudioID audioID, bool isLoop = false)
    {
        if (m_sharedSfxDict.ContainsKey(audioID))
        {
            var id = m_sharedSfxDict[audioID];
            PlaySfx(audioID, ref id);
        }
        else
        {
            ulong id = default;
            PlaySfx(audioID, ref id);
            m_sharedSfxDict.Add(audioID, id);
        }
    }

    public void StopBGM(uint id)
    {
        if (!m_bgmDict.Keys.Contains(id))
        {
            Debugger.Warning($"[BGM not found in {nameof(StopBGM)} | Class: {GetType().Name}");
            return;
        }

        m_bgmDict[id].Stop();
    }

    public void StopSfx(ulong id)
    {
        if (!m_sfxDict.Keys.Contains(id))
        {
            Debugger.Warning($"[Sfx not found in {nameof(StopSfx)} | Class: {GetType().Name}");
            return;
        }

        m_sfxDict[id]?.Stop();
    }

    private ulong m_sfxSword1;
    private ulong m_sfxSword2;
    private ulong m_sfxSword3;
    private ulong m_sfxSword4;
    private ulong m_sfxSword5;

    public void PlaySwordSfx()
    {
        int number = Random.Range(1, 6);
        switch (number)
        {
            case 1:
                PlaySfx(AudioID.SfxSword1, ref m_sfxSword1, false);
                break;
            case 2:
                PlaySfx(AudioID.SfxSword2, ref m_sfxSword2, false);
                break;
            case 3:
                PlaySfx(AudioID.SfxSword3, ref m_sfxSword3, false);
                break;
            case 4:
                PlaySfx(AudioID.SfxSword4, ref m_sfxSword4, false);
                break;
            case 5:
                PlaySfx(AudioID.SfxSword5, ref m_sfxSword5, false);
                break;
        }
    }

    private ulong m_sfxFlesh1;
    private ulong m_sfxFlesh2;
    private ulong m_sfxFlesh3;

    public void PlaySwordFleshSfx()
    {
        int number = Random.Range(1, 4);
        switch (number)
        {
            case 1:
                PlaySfx(AudioID.SfxFlesh1, ref m_sfxFlesh1);
                break;
            case 2:
                PlaySfx(AudioID.SfxFlesh2, ref m_sfxFlesh2);
                break;
            case 3:
                PlaySfx(AudioID.SfxFlesh3, ref m_sfxFlesh3);
                break;
        }
    }
}

public enum AudioID
{
    BGMMenu,
    BGMGame,
    BGMBoss,

    SfxSword1,
    SfxSword2,
    SfxSword3,
    SfxSword4,
    SfxSword5,

    SfxPlayerSelfDeath,
    SfxGrassStep,
    SfxPlayerDeath,

    SfxTeleport,

    SfxOrcRoar,
    SfxGrimRepearDeath,
    SfxGrimReaperVoice,

    SfxUnlockSkill,
    SfxEquip,
    SfxButtonClick,

    SfxFlesh1,
    SfxFlesh2,
    SfxFlesh3,
}