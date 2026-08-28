using UnityEngine;

public class Portal : MonoBehaviour
{
    private ulong _sfx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsGrimReaperDead == false) return;

        if (other.gameObject.tag == "player")
        {
            SoundManager.Instance.PlaySfx(AudioID.SfxTeleport, ref _sfx);
            EventSubscriber.OnGameWin?.Invoke();
        }
    }
}