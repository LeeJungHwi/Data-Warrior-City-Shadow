using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityGauge : MonoBehaviour
{
    [Header ("최대 게이지")] [SerializeField] private float maxGauge;
    private float curGauge; // 현재 게이지
    public float CurGauge
    {
        get { return curGauge; }
        set
        {
            // 게이지 획득
            curGauge = value > maxGauge ? maxGauge : value;

            // 스킬 게이지 UI 업데이트
            UpdateGaugeUI();
        }
    }
    [Header ("게이지 슬라이더")] [SerializeField] private Slider gaugeSlider;

    // 게이지 획득
    private void Update() { GetGauge(); }

    // 게이지 획득 : 평타 히트 시 획득, 임시로 G 키로 획득
    public void GetGauge() { if(Input.GetKeyDown(KeyCode.G)) CurGauge += 5; }

    // 스킬 게이지 사용가능 여부 체크
    public bool IsEnoughGauge(float gaugeCost) { return curGauge >= gaugeCost; }

    // 스킬 게이지 UI 업데이트
    public void UpdateGaugeUI() { gaugeSlider.value = curGauge / maxGauge; }
}
