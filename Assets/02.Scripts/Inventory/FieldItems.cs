using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;

    // �ʵ� ������ ����
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.vfxs = _item.vfxs;
    }

    // �ʵ� ������ ȹ��
    public Item GetItem()
    {
        return item;
    }

    // �ʵ� ������ �ı�
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
