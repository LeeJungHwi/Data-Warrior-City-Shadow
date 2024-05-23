using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            obj.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }
}

