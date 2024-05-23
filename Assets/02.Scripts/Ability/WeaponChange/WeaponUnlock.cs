using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using static AbilityEnum;
using static AbilityUserClass;

public class WeaponUnlock : MonoBehaviour
{
    [Header ("무기 교체")] [SerializeField] private AbilityChange abilityChange;
    public bool isFirstUnlock; // 처음 무기 획득인지 체크
    [SerializeField] [Header ("각 무기 해제 여부 및 자물쇠 이미지")] private List<WeaponUnlockImage> weaponUnlockImageList = new List<WeaponUnlockImage>();
    public Dictionary<WeaponType, WeaponUnlockImage> weaponUnlockImageMap = new Dictionary<WeaponType, WeaponUnlockImage>(); // (각 무기 타입, 각 무기 해제 여부 및 자물쇠 이미지) 맵핑

    // (각 무기 타입, 각 무기 해제 여부 및 자물쇠 이미지) 맵핑
    private void Awake() { WeaponUnlockMap(); }
    private void WeaponUnlockMap() { for(int i = 0; i < weaponUnlockImageList.Count; i++) weaponUnlockImageMap[(WeaponType)i] = weaponUnlockImageList[i]; }

    // 임시로 67890 무기 해제 => 나중에 무기 필드 아이템 획득 시 무기 해제
    private void Update() { UnlockWeapon(); }

    // 무기 해제
    private void UnlockWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha6)) UnlockLogic(WeaponType.CC);
        else if(Input.GetKeyDown(KeyCode.Alpha7)) UnlockLogic(WeaponType.DS);
        else if(Input.GetKeyDown(KeyCode.Alpha8)) UnlockLogic(WeaponType.NM);
        else if(Input.GetKeyDown(KeyCode.Alpha9)) UnlockLogic(WeaponType.PC);
        else if(Input.GetKeyDown(KeyCode.Alpha0)) UnlockLogic(WeaponType.RC);
    }

    // 무기 해제
    private void UnlockLogic(WeaponType type)
    {
        // 1.해제 한 무기로 자동 교체
        abilityChange.WeaponT = type;

        // 2.처음 무기 획득 시 Q W E 알파 255
        FirstWeaponUnlock();

        // 3.해제한 무기만 교체 가능함
        weaponUnlockImageMap[type].isUnlock = true;

        // 4.해제한 무기 자물쇠 이미지 비활성화
        weaponUnlockImageMap[type].unlockImage.gameObject.SetActive(false);
    }

    // 처음 무기 획득 시
    public void FirstWeaponUnlock()
    {
        if(isFirstUnlock) return; // 처음 획득이 아니면 리턴

        // 처음 획득
        isFirstUnlock = true;

        // Q W E 알파 255
        Color[] colors = new Color[2]; 

        for(int i = 0; i < 2; i++)
        {
            colors[i] = abilityChange.abilityImageChange.qImage[i].color;
            colors[i].a = 1;
        }

        for(int i = 0; i < 2; i++)
        {
            abilityChange.abilityImageChange.qImage[i].color = colors[i];
            abilityChange.abilityImageChange.wImage[i].color = colors[i];
            abilityChange.abilityImageChange.eImage[i].color = colors[i];
        }
    }
}
