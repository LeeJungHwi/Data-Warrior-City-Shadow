using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

// 무기 타입
public enum WeaponType
{
    CC, DS, NM, PC, RC
}

public class AbilityChange : MonoBehaviour
{
    private WeaponType weaponT; // 현재 무기 타입
    public WeaponType WeaponT
    {
        get { return weaponT; }
        set
        {
            // 무기 타입이 변경 될 때
            // 무가 타입에 해당하는 스킬로 교체
            weaponT = value;
            SkillChange();
        }
    }
    private List<AbilityFSM> fsmList = new List<AbilityFSM>(); // 플레이어가 가지고있는 3개 FSM
    [SerializeField] private List<List<AbilityBase> > abilityList = new List<List<AbilityBase> >(); // 모든 스킬 저장
    [SerializeField] [Header ("스킬 스크립터블 저장")] private List<AbilityBase> CCList = new List<AbilityBase>(); // CC 스킬
    [SerializeField] private List<AbilityBase> DSList = new List<AbilityBase>(); // DS 스킬
    [SerializeField] private List<AbilityBase> NMList = new List<AbilityBase>(); // NM 스킬
    [SerializeField] private List<AbilityBase> PCList = new List<AbilityBase>(); // PC 스킬
    [SerializeField] private List<AbilityBase> RCList = new List<AbilityBase>(); // RC 스킬
    private void Awake() { AbilityListInit(); } 

    // FSM 3개, 모든 스킬 저장
    private void AbilityListInit()
    {
        fsmList.AddRange(GetComponents<AbilityFSM>());
        abilityList.Add(CCList);
        abilityList.Add(DSList);
        abilityList.Add(NMList);
        abilityList.Add(PCList);
        abilityList.Add(RCList);
    }

    // 임시로 12345 무기 교체
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) WeaponT = WeaponType.CC;
        else if(Input.GetKeyDown(KeyCode.Alpha2)) WeaponT = WeaponType.DS;
        else if(Input.GetKeyDown(KeyCode.Alpha3)) WeaponT = WeaponType.NM;
        else if(Input.GetKeyDown(KeyCode.Alpha4)) WeaponT = WeaponType.PC;
        else if(Input.GetKeyDown(KeyCode.Alpha5)) WeaponT = WeaponType.RC;
    }

    // 스킬 교체
    private void SkillChange()
    {
        for(int i = 0; i < fsmList.Count; i++)
        {
            // 이전 무기 스킬 초기화
            PreSkillInit(i);

            // 스킬 교체
            fsmList[i].ability = abilityList[(int)weaponT][i];
        }
    }

    // 이전 무기 스킬 초기화
    private void PreSkillInit(int idx)
    {
        if(fsmList[idx].curAbilityState == AbilityState.active) fsmList[idx].ability.CastEnd();
        fsmList[idx].activeTime = 0f;
        fsmList[idx].cooldownTime = 0f;
        fsmList[idx].curAbilityState = AbilityState.ready;
    }
}
