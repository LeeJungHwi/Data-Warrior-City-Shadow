using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/NetMaker/Q")]
public class NMQactive : AsyncAbilityBase
{
    private GameObject instantAbility; // 생성된 스킬

    // 스킬 시전
    public override IEnumerator Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetSkill(AbilityType.NMQ);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 10f);

        // 사운드 풀링
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.NMQ1);

        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            AbilitySound.instance.SkillSfxPlay(AbilitySoundType.NMQ1);
        }
        
        yield return new WaitForSeconds(0.5f);
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.NMQ2);
    }

    // 스킬 종료
    public override void CastEnd()
    {
        // 스킬 반환
        AbilityPool.instance.ReturnSkill(instantAbility, AbilityType.NMQ);
    }
}
