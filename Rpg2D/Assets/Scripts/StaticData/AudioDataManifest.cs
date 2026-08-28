using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDataManifest", menuName = "Database/Audio/AudioDataManifest")]
public class AudioDataManifest : ScriptableObject
{
     public List<AudioData> audioDataList;
}