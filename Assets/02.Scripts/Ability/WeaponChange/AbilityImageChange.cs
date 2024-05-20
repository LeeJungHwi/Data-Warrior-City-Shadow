using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AbilityEnum;
using static AbilityUserClass;

public class AbilityImageChange : MonoBehaviour
{
    [SerializeField] [Header ("각 무기 스킬 이미지")] private List<AbilityImage> abilityImageList = new List<AbilityImage>();
    [Header ("변경 할 Q 스킬 칸")] public List<Image> qImage = new List<Image>();
    [Header ("변경 할 W 스킬 칸")] public List<Image> wImage = new List<Image>();
    [Header ("변경 할 E 스킬 칸")] public List<Image> eImage = new List<Image>();
    private Dictionary<WeaponType, AbilityImage> abilityImageMap = new Dictionary<WeaponType, AbilityImage>(); // (각 무기 타입, 각 무기 스킬 이미지) 맵핑

    // (각 무기 타입, 각 무기 스킬 이미지) 맵핑
    private void Awake() { AbilityImageMap(); }
    private void AbilityImageMap() { for(int i = 0; i < abilityImageList.Count; i++) abilityImageMap[(WeaponType)i] = abilityImageList[i]; }

    // 선택된 무기 스킬 이미지로 교체
    public void SelectedWeaponAbilityImage(WeaponType curType)
    {
        for(int i = 0; i < 2; i++) qImage[i].sprite = abilityImageMap[curType].Q;
        for(int i = 0; i < 2; i++) wImage[i].sprite = abilityImageMap[curType].W;
        for(int i = 0; i < 2; i++) eImage[i].sprite = abilityImageMap[curType].E;
    }
}
