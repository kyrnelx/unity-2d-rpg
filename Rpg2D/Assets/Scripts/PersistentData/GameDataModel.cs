using UnityEngine;

public class GameDataModel
{
    public float soundVolume;
    public float musicVolume;
    public int coin;

    public void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetInt("Coin", coin);
    }

    public void Load()
    {
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SoundManager.Instance.UpdateMusicVolume(musicVolume);
        SoundManager.Instance.UpdateSfxVolume(soundVolume);
        coin = PlayerPrefs.GetInt("Coin", 0);
    }

    public void AddCoin(int number)
    {
        coin += number;
    }

    public void ResetCoin()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetFloat("SoundVolume", 1);
        PlayerPrefs.SetFloat("MusicVolume", 1);
    }
}