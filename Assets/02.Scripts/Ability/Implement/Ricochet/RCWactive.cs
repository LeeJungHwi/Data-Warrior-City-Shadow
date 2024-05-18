using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ricochet/W")]
public class RCWactive : AsyncAbilityBase
{
    // 스킬 시전
    public override IEnumerator Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.RCW);
        instantAbility.transform.position = GameObject.Find("Player").transform.position;

        // 사운드 풀링
        for(int i = 0; i < 10; i++)
        {
            AbilitySound.instance.SkillSfxPlay(AbilitySoundType.RCW);
            yield return twoTenthsSecond; 
        }
    }

    // 스킬 종료
    public override void CastEnd() { AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.RCW); }
}
