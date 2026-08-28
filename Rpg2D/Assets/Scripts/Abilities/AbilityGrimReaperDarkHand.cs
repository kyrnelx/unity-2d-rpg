using UnityEngine;

/// <summary>
/// Grim Reaper 黑暗之手
/// </summary>
public class AbilityGrimReaperDarkHand : Ability
{
    public float attackRadius;
    public Vector2 AttackPosition => transform.position + Vector3.down;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.down, attackRadius);
    }
}