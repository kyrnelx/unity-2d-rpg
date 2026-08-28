using UnityEngine;

public class BossArea : MonoBehaviour
{
    public Transform teleportPosition1;
    public Transform teleportPosition2;
    private Collider2D m_cd;

    private void Awake()
    {
        m_cd = GetComponent<Collider2D>();
        OnTriggerEnter2D(null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        if (GameManager.Instance.IsGrimReaperDead) return;

        if (other.gameObject.tag == "player")
        {
            GameManager.Instance.ChallengeBoss(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (GameManager.Instance.IsGrimReaperDead) return;

        if (other.gameObject.tag == "player")
        {
            GameManager.Instance.ChallengeBoss(false);
        }
    }
}