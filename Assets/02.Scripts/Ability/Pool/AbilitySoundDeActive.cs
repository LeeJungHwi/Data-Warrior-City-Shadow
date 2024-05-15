using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySoundDeActive : MonoBehaviour
{
    private AudioSource audio; // 오디오소스
    [SerializeField] [Header ("스킬 사운드 타입")] private AbilitySoundType type; // 스킬 사운드 타입

    private void Awake() { audio = GetComponent<AudioSource>(); }

    // 사운드 재생이 끝나면 풀에 자동 반환
    private void Update() { if(!audio.isPlaying) AbilityPool.instance.ReturnSound(gameObject, type); }
}
