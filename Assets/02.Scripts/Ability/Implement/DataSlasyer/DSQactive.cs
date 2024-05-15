using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/DataSlasyer/Q")]
public class DSQactive : SyncAbilityBase
{
    [Header ("스킬 이펙트 프리팹")] public GameObject abilityPrefab;
    private GameObject instantAbility; // 생성된 스킬

    // 스킬 시전
    public override void Cast()
    {
        // 스킬 생성
        instantAbility = Instantiate(abilityPrefab, GameObject.Find("Player").transform.position + new Vector3(0, 0, 5f), abilityPrefab.transform.rotation);
    }

    // 스킬 종료
    public override void CastEnd()
    {
        // 스킬 삭제
        Destroy(instantAbility);
    }
}
