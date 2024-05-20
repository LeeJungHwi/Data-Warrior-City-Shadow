using UnityEngine;
using static AbilityEnum;

public class AbilitySound : MonoBehaviour
{
    public static AbilitySound instance;
    private void Awake() { instance = this; }

    // 스킬 사운드 재생
    public void SkillSfxPlay(AbilitySoundType type) { AbilityPool.instance.GetPool(AbilityPool.instance.queSoundMap, type); }
}  
