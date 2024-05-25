using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private InventoryExpander inven;

    public GameObject inventoryPanel;
    private bool activeInventory = false;


    public Slot[] slots;    // ���� Ȱ�� ����
    public Transform slotHolder;

    public TMP_Text slotsCnt;   // ���� �̿����� ���� ����
    public TMP_Text cache;      // ���� ������ �ݾ�

    private void Start()
    {
        inven = InventoryExpander.Instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;      // onSlotCountChange �� ������ �޼ҵ�
        inven.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
        closeShop.onClick.AddListener(DeActiveShop);
    }

    // ��� ������ ���Ը� Ȱ��ȭ
    private void SlotChange(int val)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            Button button;
            if (slots[i].TryGetComponent<Button>(out button))
            {
                button.interactable = (i < inven.SlotCnt);
            }
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab)) && !isStoreActive)
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
        if (Input.GetMouseButtonUp(0))
        {
            RayShop();
        }
    }

    // �κ��丮 ���� ���� ����
    public void AddSlot()
    {
        inven.SlotCnt++;
    }

    private void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            Debug.Log("RedrawSlot");
            slots[i].RemoveSlot();
        }
        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    public GameObject shop;
    public Button closeShop;
    public bool isStoreActive;

    public void RayShop()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10;
        //if(!UnityEngine.EventSystems.EventSystem.current )
        //RaycastHit hit = Physics.Raycast(mousePos, transform.forward, 30);
        //if(hit.collider != null)
        //{
        //    if (hit.collider.CompareTag("Store"){
        //        if (!isStoreActive)
        //        {
        //            ActiveShop(true);
        //        }
        //    }
        //}
        //Debug.DrawRay(mousePos, transform.position, Color.red, 0.5f); 
    }

    public void ActiveShop(bool isOpen)
    {
        if (!activeInventory)
        {
            isStoreActive = isOpen;
            shop.SetActive(isOpen);
            inventoryPanel.SetActive(isOpen);
        }
    }

    public void DeActiveShop()
    {
        ActiveShop(false);
    }
}

