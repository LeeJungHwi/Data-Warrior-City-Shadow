using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;

    // itemIcon.sprite를 아이템 이미지로 초기화하고 실행시켜줌
    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        Debug.Log("RemoveSlot");
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = item.Use();
        if(isUse)
        {   // 인벤토리의 items에서 알맞은 속성을 제거
            InventoryExpander.Instance.RemoveItem(slotnum);
        }
    }
}
