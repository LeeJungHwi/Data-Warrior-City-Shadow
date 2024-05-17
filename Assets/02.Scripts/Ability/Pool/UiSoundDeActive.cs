using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSoundDeActive : MonoBehaviour
{
    // 풀링 안 쓰는 사운드
    // 클립 재생이 끝나면 비활성화
    private AudioSource audioSource;
    private void Awake() { audioSource = GetComponent<AudioSource>(); }
    private void Update()
    {
        if(!audioSource.isPlaying) gameObject.SetActive(false);
    }
}
