using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataManifest", menuName = "Database/Enemy/EnemyDataManifest")]
public class EnemyDataManifest : ScriptableObject
{
    public List<EnemyData> enemyDataList;
}