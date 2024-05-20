using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private StarterAssetsInputs _input;

    [Header("Aim")] [SerializeField] private CinemachineVirtualCamera _aimCam;
    [SerializeField] private GameObject _aimImage;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        AimCheck();
    }

    private void AimCheck() //코로스헤어와 에임캠 활성화 함수
    {
        if (_input.aim)
        {
            _aimCam.gameObject.SetActive(true);
            _aimImage.SetActive(true);
        }
        else
        {
            _aimCam.gameObject.SetActive(false);
            _aimImage.SetActive(false);
        }
    }
}
