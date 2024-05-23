using UnityEngine;

public class AbilityBase : ScriptableObject
{
    [Header ("스킬 유지시간")] public float activeTime;
    [Header ("스킬 쿨시간")] public float cooldownTime;
    [Header ("스킬 소모 게이지")] public float gaugeCost;
    protected GameObject instantAbility; // 생성된 스킬

    // 스킬 종료
    public virtual void CastEnd() {}
}
