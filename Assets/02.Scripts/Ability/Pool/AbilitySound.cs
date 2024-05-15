using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySound : MonoBehaviour
{
    public static AbilitySound instance;
    private void Awake() { instance = this; }
}
