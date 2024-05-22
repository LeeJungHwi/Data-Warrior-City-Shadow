using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;

    // 필드 아이템 정보
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.vfxs = _item.vfxs;
    }

    // 필드 아이템 획득
    public Item GetItem()
    {
        return item;
    }

    // 필드 아이템 파괴
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
