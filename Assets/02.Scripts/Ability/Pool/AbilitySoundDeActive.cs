using UnityEngine;
using static AbilityEnum;

public class AbilitySoundDeActive : MonoBehaviour
{
    private AudioSource audioSource; // 오디오소스
    [SerializeField] [Header ("스킬 사운드 타입")] private AbilitySoundType type; // 스킬 사운드 타입

    private void Awake() { audioSource = GetComponent<AudioSource>(); }

    // 사운드 재생이 끝나면 풀에 자동 반환
    private void Update() { if(!audioSource.isPlaying) AbilityPool.instance.ReturnPool(AbilityPool.instance.queSoundMap, gameObject, type); }
}
