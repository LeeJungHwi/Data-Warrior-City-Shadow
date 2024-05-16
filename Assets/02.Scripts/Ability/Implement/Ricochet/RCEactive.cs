using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ricochet/E")]
public class RCEactive : AsyncAbilityBase
{
    private GameObject instantAbility; // 생성된 스킬

    // 스킬 시전
    public override IEnumerator Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.RCE);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 25f);

        // 사운드 풀링
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.RCE1);
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 20; i++)
        {
            AbilitySound.instance.SkillSfxPlay(AbilitySoundType.RCE2);
            yield return new WaitForSeconds(0.2f);
        }
    }

    // 스킬 종료
    public override void CastEnd()
    {
        // 스킬 반환
        AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.RCE);
    }
}
