using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // 1.선택된 무기 변경
            // 2.스킬 스크립터블 변경
            // 3.스킬 이미지 변경
            // 4.무기 교체 쿨타임 초기화
            // 5.스킬 툴팁 정보 교체
            // 6.무기 교체 사운드
            abilityImageChange.SelectedWeaponAbilityImage(value);
            selectedFocus.SelectedWeaponFocus(weaponT, value);
            weaponT = value;
            SkillChange();
            WeaponChangeCoolInit();
            abilityInfoChange.SetAbilityToolTipInfo(value);
            weaponChangeSound.SetActive(true);
        }
    }
    private List<AbilityFSM> fsmList = new List<AbilityFSM>(); // 플레이어가 가지고있는 3개 FSM
    [SerializeField] private List<List<AbilityBase> > abilityList = new List<List<AbilityBase> >(); // 모든 스킬 저장
    [SerializeField] [Header ("스킬 스크립터블 저장")] [Space (10f)] private List<AbilityBase> CCList = new List<AbilityBase>(); // CC 스킬
    [SerializeField] private List<AbilityBase> DSList = new List<AbilityBase>(); // DS 스킬
    [SerializeField] private List<AbilityBase> NMList = new List<AbilityBase>(); // NM 스킬
    [SerializeField] private List<AbilityBase> PCList = new List<AbilityBase>(); // PC 스킬
    [SerializeField] private List<AbilityBase> RCList = new List<AbilityBase>(); // RC 스킬
    [SerializeField] [Header ("선택된 무기 표시")] private SelectedFocus selectedFocus;
    [SerializeField] [Header ("스킬 이미지 교체")] private AbilityImageChange abilityImageChange;
    [SerializeField] [Header ("스킬 툴팁 정보 교체")] private AbilityInfoChange abilityInfoChange;
    private bool isChange = true; // 무기 교체가 가능한지 체크
    private float duration = 0f; // 무기 교체 가능시간 계산용
    [SerializeField] [Header ("무기 교체 쿨타임 시간")] private float changeTime;
    [SerializeField] [Header ("무기 교체 사운드")] private GameObject weaponChangeSound;

    // FSM 3개, 모든 스킬 저장
    private void Awake() { AbilityListInit(); }
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
        // 무기 교체 쿨타임
        if(!isChange)
        {
            duration += Time.deltaTime;
            if(duration > changeTime) isChange = true;
            return;
        }

        // 무기 교체
        if(Input.GetKeyDown(KeyCode.Alpha1)) WeaponT = WeaponType.CC;
        else if(Input.GetKeyDown(KeyCode.Alpha2)) WeaponT = WeaponType.DS;
        else if(Input.GetKeyDown(KeyCode.Alpha3)) WeaponT = WeaponType.NM;
        else if(Input.GetKeyDown(KeyCode.Alpha4)) WeaponT = WeaponType.PC;
        else if(Input.GetKeyDown(KeyCode.Alpha5)) WeaponT = WeaponType.RC;
    }

    // 스킬 스크립터블 교체
    private void SkillChange()
    {
        for(int i = 0; i < fsmList.Count; i++)
        {
            // 이전 무기 스킬 초기화
            PreSkillInit(i);

            // 스킬 스크립터블 교체
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
        fsmList[idx].duration = 0f;
        fsmList[idx].cooldownImage.fillAmount = 1f;
    }

    // 무기 교체 쿨타임 초기화
    private void WeaponChangeCoolInit()
    {
        isChange = false;
        duration = 0f;
    }
}
