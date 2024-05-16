using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMEcollision : MonoBehaviour
{
    // 스킬 충돌처리
    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            // 충돌처리
            Debug.Log("Enemy was hit!");

            // 충돌 사운드
            AbilitySound.instance.SkillSfxPlay(AbilitySoundType.NME2);
        }
    }
}
