using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ricochet/Q")]
public class RCQactive : SyncAbilityBase
{
    private GameObject instantAbility; // 생성된 스킬

    // 스킬 시전
    public override void Cast()
    {
        // 스킬 풀링
        instantAbility = AbilityPool.instance.GetObj(AbilityType.RCQ);
        instantAbility.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, 0, 5f);
        instantAbility.transform.rotation = GameObject.Find("Player").transform.rotation;
    }

    // 스킬 종료
    public override void CastEnd()
    {
        // 스킬 반환
        AbilityPool.instance.ReturnObj(instantAbility, AbilityType.RCQ);
    }
}
