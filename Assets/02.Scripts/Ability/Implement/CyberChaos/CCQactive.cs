using System.Collections;
using UnityEngine;
using static AbilityEnum;

[CreateAssetMenu(menuName = "Ability/CyberChaos/Q")]
public class CCQactive : AsyncAbilityBase
{
    // 스킬 시전
    public override IEnumerator Cast()
    {
        // 사운드 풀링
        // CCQ 시전 후 1초 안에 무기 바꾸면 널에러 이슈
        // 아직 instantAbility 풀링 하지 않았는데 무기 바꿔서 CastEnd 호출됨
        AbilitySound.instance.SkillSfxPlay(AbilitySoundType.CCQ);
        yield return oneSecond;

        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetPool(AbilityPool.instance.queMap, AbilityType.CCQ);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 25f);
    }

    // 스킬 종료
    public override void CastEnd() { AbilityPool.instance.ReturnPool(AbilityPool.instance.queMap, instantAbility, AbilityType.CCQ); }
}
