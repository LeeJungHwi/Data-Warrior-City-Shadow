using System.Collections;
using UnityEngine;
using static AbilityEnum;

[CreateAssetMenu(menuName = "Ability/Ricochet/Q")]
public class RCQactive : AsyncAbilityBase
{
    // 스킬 시전
    public override IEnumerator Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.RCQ);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 5f);

        // 사운드 풀링
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.RCQ1);
        yield return fourTenthsSecond;

        for(int i = 0; i < 10; i++)
        {
            AbilitySound.instance.SkillSfxPlay(AbilitySoundType.RCQ2);
            yield return twoTenthsSecond;
        }
    }

    // 스킬 종료
    public override void CastEnd() { AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.RCQ); }
}
