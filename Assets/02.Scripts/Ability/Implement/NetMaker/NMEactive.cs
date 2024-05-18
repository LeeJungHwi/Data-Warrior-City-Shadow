using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/NetMaker/E")]
public class NMEactive : SyncAbilityBase
{
    // 스킬 시전
    public override void Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.NME);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 30f);

        // 사운드 풀링
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.NME1);
    }

    // 스킬 종료
    public override void CastEnd() { AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.NME); }
}
