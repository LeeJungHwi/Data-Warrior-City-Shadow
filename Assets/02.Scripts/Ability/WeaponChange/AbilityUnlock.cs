using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilityEnum;
using static AbilityUserClass;

public class AbilityUnlock : MonoBehaviour
{
    [Header ("무기 교체")] [SerializeField] private AbilityChange abilityChange;
    [SerializeField] [Header ("각 무기 W E 해제 상태 관리")] private List<IsAbilityUnlock> isAbilityUnlockList = new List<IsAbilityUnlock>();
    private Dictionary<WeaponType, IsAbilityUnlock> abilityUnlockMap = new Dictionary<WeaponType, IsAbilityUnlock>(); // (각 무기 타입, 각 무기 W E 해제 상태) 맵핑
    [Header ("스킬 자물쇠 이미지")] public List<GameObject> abilityLockImage = new List<GameObject>();
    [Header ("스킬 해제 버튼")] public List<GameObject> abilityUnlockBtn = new List<GameObject>();

    // (각 무기 타입, 각 무기 W E 해제 상태) 맵핑
    private void Awake() { AbilityUnlockMap(); }
    private void AbilityUnlockMap() { for(int i = 0; i < isAbilityUnlockList.Count; i++) abilityUnlockMap[(WeaponType)i] = isAbilityUnlockList[i]; }

    // 스킬 해제
    // 0 : W, 1 : E
    public void AbilityUnlockFunc(int abilityIdx)
    {
        // 아직 무기를 획득하지 않았으면 리턴
        if(!abilityChange.weaponUnlock.isFirstUnlock) return;

        // 현재 무기의 스킬 해제
        abilityUnlockMap[abilityChange.WeaponT].isUnlockAbility[abilityIdx] = true;

        // 자물쇠, 스킬 해제 버튼 비활성화
        abilityLockImage[abilityIdx].SetActive(false);
        abilityUnlockBtn[abilityIdx].SetActive(false);
    }

    // 무기 교체 시 스킬 해제 상태 체크하고
    // 자물쇠 이미지 및 스킬 해제 버튼 관리
    public void AbilityUnlockStateCheck(WeaponType type)
    {
        for(int i = 0; i < 2; i++)
        {
            abilityLockImage[i].SetActive(!abilityUnlockMap[type].isUnlockAbility[i]);
            abilityUnlockBtn[i].SetActive(!abilityUnlockMap[type].isUnlockAbility[i]);
        }
    }
}
