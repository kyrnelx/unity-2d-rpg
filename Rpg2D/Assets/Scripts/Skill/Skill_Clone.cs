using UnityEngine;

public class Skill_Clone : Skill
{
    public GameObject clonePrefab;
    public ObjectPool<PlayerCloneController> playerClonePool = new ObjectPool<PlayerCloneController>();

    public override void Release(params object[] parameters)
    {
        base.Release(parameters);
        PlayerCloneController playerCloneController = playerClonePool.Get();
        if (playerCloneController == null)
        {
            GameObject playerClone = Instantiate(clonePrefab);
            playerCloneController = playerClone.GetComponent<PlayerCloneController>();
            playerCloneController.callback = playerClonePool.Set;
        }

        Entity target = (Entity)parameters[0];
        playerCloneController.transform.position = target.transform.position + Vector3.right * -target.facingDir;
        playerCloneController.transform.rotation =
            target.facingDir == -1 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        playerCloneController.Release();
    }

    ~Skill_Clone()
    {
        while (playerClonePool.pool.Count > 0)
        {
            Destroy(playerClonePool.pool.Dequeue().gameObject);
        }

        playerClonePool.pool.Clear();
    }
}