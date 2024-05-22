using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private StarterAssetsInputs _input;

    [Header("Aim")] [SerializeField] private CinemachineVirtualCamera aimCam;
    [SerializeField] private GameObject aimImage;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject aimObj;
    [SerializeField] private float aimObjDis = 20;
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
            aimCam.gameObject.SetActive(true);
            aimImage.SetActive(true);

            Vector3 targetPosition = Vector3.zero; //레이케스트 충돌 위치 초기화
            Transform camTrans = Camera.main.transform;
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position,camTrans.forward,out hit,Mathf.Infinity,targetLayer))
            {
                Debug.Log("HitObjectName: "+hit.transform.gameObject.name);
                //레이어를 통해 추 후 설정해야함.
                targetPosition = hit.point;
                aimObj.transform.position = hit.point;
            }
            else //원하는 타깃이 아닌 경우 카메라가 바라보는 방향으로 위치 설정
            {
                targetPosition = camTrans.position + camTrans.forward * aimObjDis;
                aimObj.transform.position = camTrans.position + camTrans.forward * aimObjDis;
            }
            Vector3 targetAim = targetPosition; //충돌위치 에임
            targetAim.y = transform.position.y; //캐릭터 y값, 조준점 y값 맞추기
            Vector3 aimDir = (targetAim - transform.position).normalized; //방향 단위벡터
            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime* 50f); //캐릭터전방바라보기
        }
        else
        {
            aimCam.gameObject.SetActive(false);
            aimImage.SetActive(false);
        }
    }
}
