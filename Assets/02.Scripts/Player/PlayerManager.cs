using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private StarterAssetsInputs _input;

    [Header("Aim")] [SerializeField] private CinemachineVirtualCamera _aimCam;
    
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.aim)
        {
            _aimCam.gameObject.SetActive(true);
        }
        else
        {
            _aimCam.gameObject.SetActive(false);
        }
    }
}
