using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/CyberChaos/W")]
public class CCWactive : SyncAbilityBase
{
    private GameObject instantAbility; // 생성된 스킬

    // 스킬 시전
    public override void Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.CCW);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 5f);

        // 사운드 풀링
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.CCW);
    }

    // 스킬 종료
    public override void CastEnd()
    {
        // 스킬 반환
        AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.CCW);
    }
}
