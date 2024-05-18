using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/NetMaker/W")]
public class NMWactive : AsyncAbilityBase
{
    // 스킬 시전
    public override IEnumerator Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.NMW);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 5f);

        // 사운드 풀링
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.NMW);

        for(int i = 0; i < 4; i++)
        {
            yield return twoTenthsSecond;
            AbilitySound.instance.SkillSfxPlay(AbilitySoundType.NMW);
        }
    }

    // 스킬 종료
    public override void CastEnd() { AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.NMW); }
}
