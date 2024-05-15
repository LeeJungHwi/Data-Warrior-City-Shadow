using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

// 스킬 타입
public enum AbilityType
{
    CCQ, CCW, CCE,
    DSQ, DSW, DSE,
    NMQ, NMW, NME,
    PCQ, PCW, PCE,
    RCQ, RCW, RCE
}

public class AbilityPool : MonoBehaviour
{
    public static AbilityPool instance;
    [Header ("맵핑 필드")]
    private List<List<GameObject> > AbilityList = new List<List<GameObject> >(); // 모든 스킬 저장
    [SerializeField] private List<GameObject> CCList = new List<GameObject>(); // CC 스킬
    [SerializeField] private List<GameObject> DSList = new List<GameObject>(); // DS 스킬
    [SerializeField] private List<GameObject> NMList = new List<GameObject>(); // NM 스킬
    [SerializeField] private List<GameObject> PCList = new List<GameObject>(); // PC 스킬
    [SerializeField] private List<GameObject> RCList = new List<GameObject>(); // RC 스킬
    private Dictionary<AbilityType, GameObject> prefMap = new Dictionary<AbilityType, GameObject>(); // (타입, 프리팹) 맵핑
    private Dictionary<AbilityType, Queue<GameObject> > queMap = new Dictionary<AbilityType, Queue<GameObject> >(); // (타입, 큐) 맵핑

    private void Awake()
    {
        // 정적
        instance = this;

        // 모든 스킬 저장
        AbilityListInit();

        // (타입, 프리팹) 맵핑
        PrefMap(); 

        // (타입, 큐) 맵핑
        QueMap();
    }

    // 모든 스킬 저장
    private void AbilityListInit()
    {
        AbilityList.Add(CCList);
        AbilityList.Add(DSList);
        AbilityList.Add(NMList);
        AbilityList.Add(PCList);
        AbilityList.Add(RCList);
    }

    // (타입, 프리팹) 맵핑
    private void PrefMap()
    {
        // 현재 맵핑할 스킬 타입에 따라 프리팹 맵핑
        AbilityType curAbilityType = 0;

        for(int i = 0; i < AbilityList.Count; i++)
        {
            for(int j = 0; j < AbilityList[i].Count; j++)
            {
                prefMap.Add(curAbilityType, AbilityList[i][j]);
                curAbilityType++;
            }
        }
    }

    // (타입, 큐) 맵핑
    private void QueMap(int cnt = 10)
    {   
        // 정의된 스킬 타입을 하나씩 꺼내서
        foreach(AbilityType abilityType in Enum.GetValues(typeof(AbilityType)))
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            GameObject prefab = prefMap[abilityType];

            // cnt 개 생성하고 비활성화 후 큐에 저장
            for(int i = 0; i < cnt; i++)
            {
                // 프리팹 생성
                GameObject obj = Instantiate(prefab);

                // 비활성화
                obj.SetActive(false);

                // 큐에 저장
                queue.Enqueue(obj);
            }

            // (타입, 큐) 맵핑
            queMap.Add(abilityType, queue);
        }
    }

    // 풀에서 꺼냄
    public GameObject GetObj(AbilityType type)
    {
        // 키가 존재하고 큐에 오브젝트가 있으면 꺼냄
        if(queMap.ContainsKey(type) && queMap[type].Count > 0)
        {
            // 오브젝트 꺼내서
            GameObject obj = queMap[type].Dequeue();

            // 오브젝트 활성화
            obj.SetActive(true);

            // 오브젝트 반환
            return obj;
        }

        // 사용 가능한 오브젝트가 없으면
        return null;
    }

    // 풀에 반환함
    public void ReturnObj(GameObject obj, AbilityType type)
    {
        // 오브젝트 비활성화
        obj.SetActive(false);

        // 해당 타입의 풀로 반환
        queMap[type].Enqueue(obj);
    }
}
