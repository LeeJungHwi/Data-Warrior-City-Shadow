using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryExpander : Singleton<InventoryExpander>
{
    public delegate void OnSlotCountChange(int val);
    public event OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();              // �������� �߰��Ǹ� ���� UI���� �߰�
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();     // ȹ���� ������ ���� ����Ʈ

    private int _slotCnt;   // Ȱ��ȭ�� ���� ����
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
        base.Awake(); // �θ� Ŭ������ Awake �޼ҵ� ȣ��
        if (Instance != this)
        {
            Destroy(gameObject); // �� ������Ʈ�� �̱��� �ν��Ͻ��� �ƴϸ� �ı�
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
