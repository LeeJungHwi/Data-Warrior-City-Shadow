using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static AbilityEnum;

public class AbilityFSM : MonoBehaviour
{
    [Header ("시전 할 스킬")] public AbilityBase ability;
    [Header ("스킬 사용 키")] public KeyCode activeKey;
    [HideInInspector] public float activeTime, cooldownTime; // 스킬 유지시간, 스킬 쿨시간
    [HideInInspector] public AbilityState curAbilityState = AbilityState.ready; // 현재 스킬 상태
    [Header ("스킬 쿨타임 이미지")] public Image cooldownImage;
    [HideInInspector] public float duration = 0f; // 스킬 쿨타임 계산용
    [Header ("스킬 자물쇠 이미지")] [SerializeField] private GameObject abilityLockImage; // 스킬 자물쇠 이미지
    [Header ("스킬 게이지")] [SerializeField] private AbilityGauge abilityGauge;
    [Header ("스킬 시전 가능 이펙트")] [SerializeField] private GameObject castable;

    // 스킬 FSM : 준비 => 유지 => 쿨다운
    private void Update()
    {
        switch(curAbilityState)
        {   
            // 준비
            case AbilityState.ready :
                if(IsCastAbility()) Ready();
                break;

            // 유지
            case AbilityState.active :
                Active();
                break;

            // 쿨다운
            case AbilityState.cooldown :
                Cooldown();
                break;
        }

        // 시전 가능 스킬 체크
        CastableAbility();
    }

    // 스킬 시전 조건 체크
    // 1. 스킬 키 입력
    // 2. 스킬 스크립터블 할당
    // 3. 자물쇠 비활성화
    // 4. 게이지 체크
    private bool IsCastAbility() { return Input.GetKeyDown(activeKey) && ability != null && !abilityLockImage.activeSelf && abilityGauge.IsEnoughGauge(ability.gaugeCost); }

    // 준비
    private void Ready()
    {
        // 스킬 시전
        if(ability is SyncAbilityBase syncAbilityBase) syncAbilityBase.Cast();
        else if(ability is AsyncAbilityBase asyncAbilityBase) StartCoroutine(asyncAbilityBase.Cast());

        // 스킬 게이지 소모
        abilityGauge.CurGauge -= ability.gaugeCost;

        // 스킬 유지 상태
        curAbilityState = AbilityState.active;

        // 스킬 시간 초기화
        activeTime = ability.activeTime;
        cooldownTime = ability.cooldownTime;
        duration = 0f;
    }

    // 유지
    private void Active()
    {
        if(activeTime > 0)
        {
            // 스킬 유지시간 감소
            activeTime -= Time.deltaTime;

            // 스킬 쿨타임 이미지 갱신
            UpdateCooldownUI();

            return;
        }

        // 스킬 종료
        ability.CastEnd();

        // 스킬 쿨다운 상태
        curAbilityState = AbilityState.cooldown;
    }

    // 쿨다운
    private void Cooldown()
    {
        if(cooldownTime > 0)
        {
            // 스킬 쿨다운시간 감소
            cooldownTime -= Time.deltaTime;

            // 스킬 쿨타임 이미지 갱신
            UpdateCooldownUI();

            return;
        }
        
        // 스킬 준비 상태
        curAbilityState = AbilityState.ready;
    }

    // 스킬 쿨타임 이미지 갱신
    private void UpdateCooldownUI()
    {
        duration += Time.deltaTime;
        cooldownImage.fillAmount = duration / (ability.activeTime + ability.cooldownTime);
    }

    // 시전 가능 스킬 체크
    private void CastableAbility()
    {
        if(ability == null) return;
        castable.SetActive(!castable.activeSelf ? (abilityGauge.IsEnoughGauge(ability.gaugeCost) && !abilityLockImage.activeSelf ? true : false) : (!abilityGauge.IsEnoughGauge(ability.gaugeCost) || abilityLockImage.activeSelf ? false : true));
    }
}
