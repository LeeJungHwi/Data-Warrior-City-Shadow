using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemVFX/Consumable/Health")]
public class ItemHealingVFX : ItemEffect
{
    public int healingPoint = 0;
    public override bool ExecuteRole()
    {
        Debug.Log("PlayerHp Add:" + healingPoint);
        return true;
    }
}
