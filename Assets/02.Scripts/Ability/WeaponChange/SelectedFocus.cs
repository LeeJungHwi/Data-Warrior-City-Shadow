using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 선택된 무기 프레임 이미지
[System.Serializable]
public class SelectedFrameImage
{
    public Image weaponFrame, changeFrame; // 무기프레임, 무기교체프레임

    // 생성자
    public SelectedFrameImage(Image wFrame, Image cFrame)
    {
        weaponFrame = wFrame;
        changeFrame = cFrame;
    }
}

public class SelectedFocus : MonoBehaviour
{
    [SerializeField] [Header ("각 무기 프레임 이미지")] private List<SelectedFrameImage> selectedFrameImageList = new List<SelectedFrameImage>();
    private Dictionary<WeaponType, SelectedFrameImage> selectedFrameImageMap = new Dictionary<WeaponType, SelectedFrameImage>(); // (각 무기 타입, 각 프레임 이미지) 맵핑
    [SerializeField] [Header ("선택된 무기 표시 할 때 사용할 스프라이트")] private List<Sprite> focusSpriteList = new List<Sprite>();
    [SerializeField] [Header ("선택 안 된 무기 표시 할 때 사용 할 스프라이트")] private List<Sprite> nonFocusSpriteList = new List<Sprite>();

    // (각 무기 타입, 각 프레임 이미지) 맵핑
    private void Awake() { SelectedFrameMap(); }
    private void SelectedFrameMap()
    {
        for(int i = 0; i < selectedFrameImageList.Count; i++)
        {
            selectedFrameImageMap[(WeaponType)i] = selectedFrameImageList[i];
        }
    }

    // 선택된 무기 표시
    public void SelectedWeaponFocus(WeaponType preType, WeaponType curType)
    {
        // 이전 무기 프레임 강조 해제
        selectedFrameImageMap[preType].weaponFrame.sprite = nonFocusSpriteList[0];
        selectedFrameImageMap[preType].changeFrame.sprite = nonFocusSpriteList[1];

        // 현재 무기 프레임 강조
        selectedFrameImageMap[curType].weaponFrame.sprite = focusSpriteList[0];
        selectedFrameImageMap[curType].changeFrame.sprite = focusSpriteList[1];
    }
}
