using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Database/Audio/AudioData")]
public class AudioData : ScriptableObject
{
    public AudioID audioID;
    public AudioClip audioClip;

#if UNITY_EDITOR
    protected void OnValidate()
    {
        name = audioID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}