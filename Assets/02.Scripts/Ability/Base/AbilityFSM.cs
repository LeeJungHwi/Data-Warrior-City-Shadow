using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스킬 상태
public enum AbilityState
{
    ready, active, cooldown
}

public class AbilityFSM : MonoBehaviour
{
    [Header ("시전 할 스킬")] public AbilityBase ability;
    [Header ("스킬 사용 키")] public KeyCode activeKey;
    [HideInInspector] public float activeTime, cooldownTime; // 스킬 유지시간, 스킬 쿨시간
    [HideInInspector] public AbilityState curAbilityState = AbilityState.ready; // 현재 스킬 상태
    [Header ("스킬 쿨타임 이미지")] public Image cooldownImage;
    [HideInInspector] public float duration = 0f; // 스킬 쿨타임 계산용

    // 스킬 FSM : 준비 => 유지 => 쿨다운
    private void Update()
    {
        switch(curAbilityState)
        {   
            // 준비 상태
            case AbilityState.ready :
                if(Input.GetKeyDown(activeKey))
                {
                    // 스킬 시전
                    if(ability is SyncAbilityBase syncAbilityBase) syncAbilityBase.Cast();
                    else if(ability is AsyncAbilityBase asyncAbilityBase) StartCoroutine(asyncAbilityBase.Cast());

                    // 스킬 유지 상태
                    curAbilityState = AbilityState.active;

                    // 스킬 시간 초기화
                    activeTime = ability.activeTime;
                    cooldownTime = ability.cooldownTime;
                    duration = 0f;
                }
                break;

            // 유지 상태
            case AbilityState.active :
                if(activeTime > 0)
                {
                    // 스킬 유지시간 감소
                    activeTime -= Time.deltaTime;

                    // 스킬 쿨타임 이미지 표시
                    duration += Time.deltaTime;
                    cooldownImage.fillAmount = duration / (ability.activeTime + ability.cooldownTime);
                }
                else
                {
                    // 스킬 종료
                    ability.CastEnd();

                    // 스킬 쿨다운 상태
                    curAbilityState = AbilityState.cooldown;
                }
                break;

            // 쿨다운 상태
            case AbilityState.cooldown :
                if(cooldownTime > 0)
                {
                    // 스킬 쿨다운시간 감소
                    cooldownTime -= Time.deltaTime;

                    // 스킬 쿨타임 이미지 표시
                    duration += Time.deltaTime;
                    cooldownImage.fillAmount = duration / (ability.activeTime + ability.cooldownTime);
                }
                else
                {
                    // 스킬 준비 상태
                    curAbilityState = AbilityState.ready;
                }
                break;
        }
    }
}
