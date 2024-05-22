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

    // itemIcon.sprite�� ������ �̹����� �ʱ�ȭ�ϰ� ���������
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
        {   // �κ��丮�� items���� �˸��� �Ӽ��� ����
            InventoryExpander.Instance.RemoveItem(slotnum);
        }
    }
}
