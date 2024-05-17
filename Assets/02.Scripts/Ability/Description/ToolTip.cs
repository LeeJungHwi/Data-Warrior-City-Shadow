using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTip : MonoBehaviour
{
    [Header ("이름 텍스트")] [SerializeField] private TextMeshProUGUI nameText;
    [Header ("아이콘 이미지")] [SerializeField] private Image iconImage;
    [Header ("설명 텍스트")] [SerializeField] private TextMeshProUGUI DescText;

    // 툴팁 패널 마우스 위치로
    private void Update() { transform.position = Input.mousePosition; } 

    // 툴팁 정보 갱신
    public void SetToolTipInfo(string itemName, Sprite itemIcon, string itemDesc)
    {
        nameText.text = itemName;
        iconImage.sprite = itemIcon;
        DescText.text = itemDesc;
    }
}
