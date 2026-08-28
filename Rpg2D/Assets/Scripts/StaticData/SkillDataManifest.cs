using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataManifest", menuName = "Database/Skill/SkillDataManifest")]
public class SkillDataManifest : ScriptableObject
{
     public List<SkillData> SkillDataList;
}