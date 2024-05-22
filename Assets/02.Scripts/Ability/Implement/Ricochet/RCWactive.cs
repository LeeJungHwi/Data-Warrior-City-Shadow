using System.Collections;
using UnityEngine;
using static AbilityEnum;

[CreateAssetMenu(menuName = "Ability/Ricochet/W")]
public class RCWactive : AsyncAbilityBase
{
    // 스킬 시전
    public override IEnumerator Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetPool(AbilityPool.instance.queMap, AbilityType.RCW);
        instantAbility.transform.position = GameObject.Find("Player").transform.position;

        // 사운드 풀링
        for(int i = 0; i < 10; i++)
        {
            AbilitySound.instance.SkillSfxPlay(AbilitySoundType.RCW);
            yield return twoTenthsSecond; 
        }
    }

    // 스킬 종료
    public override void CastEnd() { AbilityPool.instance.ReturnPool(AbilityPool.instance.queMap, instantAbility, AbilityType.RCW); }
}
