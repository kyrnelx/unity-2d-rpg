using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public int price;
    public bool isUnlocked;
    public float cooldown;
    public float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    public virtual void Setup(params object[] parameters)
    {
    }

    public bool CanRelease()
    {
        if (isUnlocked == false) return false;

        if (cooldownTimer > cooldown)
        {
            cooldownTimer = 0;
            return true;
        }

        return false;
    }

    public virtual void Release(params object[] parameters)
    {
    }
}