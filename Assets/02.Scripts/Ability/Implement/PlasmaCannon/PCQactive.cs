using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/PlasmaCannon/Q")]
public class PCQactive : SyncAbilityBase
{
    // 스킬 시전
    public override void Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.PCQ);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 5f);

        // 사운드 풀링
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.PCQ);
    }

    // 스킬 종료
    public override void CastEnd() { AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.PCQ); }
}
