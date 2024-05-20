using System.Collections.Generic;
using UnityEngine;
using static AbilityEnum;
using static AbilityUserClass;

public class AbilityInfoChange : MonoBehaviour
{
    [SerializeField] [Header ("변경 할 툴팁 정보 칸")] private List<ToolTipInfo> qweImage = new List<ToolTipInfo>();
    [SerializeField] [Header ("각 무기 스킬 정보")] private List<AbilityInfo> abilityInfoList = new List<AbilityInfo>();
    private Dictionary<WeaponType, AbilityInfo> abilityInfoMap = new Dictionary<WeaponType, AbilityInfo>(); // (각 무기 타입, 각 무기 스킬 정보) 맵핑

    // (각 무기 타입, 각 무기 스킬 정보) 맵핑
    private void Awake() { AbilityInfoMap(); }
    private void AbilityInfoMap() { for(int i = 0; i < abilityInfoList.Count; i++) abilityInfoMap[(WeaponType)i] = abilityInfoList[i]; }

    // 각 무기에 따라 툴팁 정보 갱신
    public void SetAbilityToolTipInfo(WeaponType curType)
    {
        for(int i = 0; i < 3; i++)
        {
            qweImage[i].itemName = abilityInfoMap[curType].abilityName[i];
            qweImage[i].itemIcon = abilityInfoMap[curType].abilityIcon[i];
            qweImage[i].itemDesc = abilityInfoMap[curType].abilityDesc[i];
        }
    }
}
