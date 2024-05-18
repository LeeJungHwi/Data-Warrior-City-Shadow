using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 선택된 무기 스킬 이미지
[System.Serializable]
public class AbilityImage
{
    public Sprite Q, W, E; // Q W E

    // 생성자
    public AbilityImage(Sprite q, Sprite w, Sprite e)
    {
        Q = q;
        W = w;
        E = e;
    }
}

public class AbilityImageChange : MonoBehaviour
{
    [SerializeField] [Header ("각 무기 스킬 이미지")] private List<AbilityImage> abilityImageList = new List<AbilityImage>();
    [SerializeField] [Header ("변경 할 Q 스킬 칸")] private List<Image> qImage = new List<Image>();
    [SerializeField] [Header ("변경 할 W 스킬 칸")] private List<Image> wImage = new List<Image>();
    [SerializeField] [Header ("변경 할 E 스킬 칸")] private List<Image> eImage = new List<Image>();
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
