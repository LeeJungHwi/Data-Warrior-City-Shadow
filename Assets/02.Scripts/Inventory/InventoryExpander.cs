using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryExpander : Singleton<InventoryExpander>
{
    public delegate void OnSlotCountChange(int val);
    public event OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();              // 아이템이 추가되면 슬롯 UI에도 추가
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();     // 획득한 아이템 담을 리스트

    private int _slotCnt;   // 활성화된 슬롯 개수
    public int SlotCnt
    {
        get => _slotCnt;
        set
        {
            _slotCnt = value;
            if (onSlotCountChange != null)
            {
                foreach (OnSlotCountChange handler in onSlotCountChange.GetInvocationList())
                {
                    handler(_slotCnt);
                }
            }
        }
    }

    void Start()
    {
        SlotCnt = 8;
    }

    protected override void Awake()
    {
        base.Awake(); // 부모 클래스의 Awake 메소드 호출
        if (Instance != this)
        {
            Destroy(gameObject); // 이 오브젝트가 싱글턴 인스턴스가 아니면 파괴
            return;
        }
    }

    public bool AddItem(Item _item)
    {
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)
            {
                foreach (OnChangeItem handler in onChangeItem.GetInvocationList())
                {
                    handler();
                }
            }
            return true;
        }
        return false;
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        if (onChangeItem != null)
        {
            foreach (OnChangeItem handler in onChangeItem.GetInvocationList())
            {
                handler();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
            {
                fieldItems.DestroyItem();
            }
        }
    }
}
