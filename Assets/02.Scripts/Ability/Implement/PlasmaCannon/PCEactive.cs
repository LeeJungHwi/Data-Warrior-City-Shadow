using System.Collections;
using UnityEngine;
using static AbilityEnum;

[CreateAssetMenu(menuName = "Ability/PlasmaCannon/E")]
public class PCEactive : AsyncAbilityBase
{
    // 스킬 시전
    public override IEnumerator Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.PCE);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 15f, 15f);

        // 사운드 풀링
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.PCE1);
        yield return eightTenthsSecond;

        for(int i = 0; i < 10; i++)
        {
            AbilitySound.instance.SkillSfxPlay(AbilitySoundType.PCE2);
            yield return twoTenthsSecond;
        }
    }

    // 스킬 종료
    public override void CastEnd() { AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.PCE); }
}
