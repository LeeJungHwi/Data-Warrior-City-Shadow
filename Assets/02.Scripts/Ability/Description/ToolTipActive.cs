using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipActive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] [Header ("툴팁 패널")] private ToolTip toolTip;
    [SerializeField] [Header ("툴팁 사운드")] private GameObject toolTipSound;

    // 툴팁 패널 활성화
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 아이템 정보 가져옴
        ToolTipInfo itemInfo = GetComponent<ToolTipInfo>();

        // 아이템 정보 셋
        if(itemInfo == null) return; // 아이템 정보가 없으면 리턴
        toolTip.gameObject.SetActive(true); // 툴팁 패널 활성화
        toolTip.SetToolTipInfo(itemInfo.itemName, itemInfo.itemIcon, itemInfo.itemDesc); // 아이템 정보 갱신
        toolTipSound.SetActive(true); // 툴팁 사운드
    }

    // 툴팁 패널 비활성화
    public void OnPointerExit(PointerEventData eventData) { toolTip.gameObject.SetActive(false); }
}
