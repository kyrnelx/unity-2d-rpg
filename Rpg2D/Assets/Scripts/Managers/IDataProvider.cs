using UnityEngine.Events;

public interface IDataProvider
{
    public int Coin { get; set; }
    public float SoundVolume { get; set; }
    public float MusicVolume { get; set; }
    public void SetJSONData();
    public void SaveGameData(UnityAction callback = null);
    public void ClearJSONData();
    public void DeleteFile();
}