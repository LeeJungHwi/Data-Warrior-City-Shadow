using System;
using System.Collections.Generic;
using System.ComponentModel;
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

// 사운드 타입
public enum AbilitySoundType
{
    CCQ, CCW, CCE1, CCE2,
    DSQ, DSW, DSE1, DSE2,
    NMQ1, NMQ2, NMW, NME1, NME2,
    PCQ, PCW, PCE1, PCE2,
    RCQ1, RCQ2, RCW, RCE1, RCE2
}

// 게임 오브젝트 형 리스트를 가지는 클래스
[Serializable]
public class ListGameObject
{
    public List<GameObject> gameObjectList;

    // 생성자
    public ListGameObject() {}
    public ListGameObject(List<GameObject> gameObjList) { gameObjectList = gameObjList; }
}


public class AbilityPool : MonoBehaviour
{
    public static AbilityPool instance;
    public GameObject poolSet; // 오브젝트 부모
    [Header ("스킬 맵핑")] [SerializeField] private List<ListGameObject> AbilityList = new List<ListGameObject>(); // 각 스킬 리스트 모두 저장
    [SerializeField] private ListGameObject CCList = new ListGameObject(), DSList = new ListGameObject(), NMList = new ListGameObject(), PCList = new ListGameObject(), RCList = new ListGameObject(); // 각 CC, DS, NM, PC, RC 스킬 리스트
    private Dictionary<AbilityType, GameObject> prefMap = new Dictionary<AbilityType, GameObject>(); // (타입, 프리팹) 맵핑
    private Dictionary<AbilityType, Queue<GameObject> > queMap = new Dictionary<AbilityType, Queue<GameObject> >(); // (타입, 큐) 맵핑
    [Header ("사운드 맵핑")] [SerializeField] private List<ListGameObject> AbilitySoundList = new List<ListGameObject>(); // 각 스킬 사운드 리스트 모두 저장
    [SerializeField] private ListGameObject CCSoundList = new ListGameObject(), DSSoundList = new ListGameObject(), NMSoundList = new ListGameObject(), PCSoundList = new ListGameObject(), RCSoundList = new ListGameObject(); // 각 스킬 사운드 리스트
    private Dictionary<AbilitySoundType, GameObject> prefSoundMap = new Dictionary<AbilitySoundType, GameObject>(); // (타입, 프리팹) 맵핑
    private Dictionary<AbilitySoundType, Queue<GameObject> > queSoundMap = new Dictionary<AbilitySoundType, Queue<GameObject> >(); // (타입, 큐) 맵핑

    private void Awake()
    {
        // 정적
        instance = this;

        // 모든 스킬 저장
        // 모든 사운드 저장
        AbilityListInit();

        // (타입, 프리팹) 맵핑
        PrefMap(AbilityList, prefMap); 
        PrefMap(AbilitySoundList, prefSoundMap);

        // (타입, 큐) 맵핑
        QueMap(queMap, prefMap);
        QueMap(queSoundMap, prefSoundMap);
    }

    // 모든 스킬 저장
    // 모든 사운드 저장
    private void AbilityListInit()
    {
        // 모든 스킬 저장
        AbilityList.Add(CCList);
        AbilityList.Add(DSList);
        AbilityList.Add(NMList);
        AbilityList.Add(PCList);
        AbilityList.Add(RCList);

        // 모든 사운드 저장
        AbilitySoundList.Add(CCSoundList);
        AbilitySoundList.Add(DSSoundList);
        AbilitySoundList.Add(NMSoundList);
        AbilitySoundList.Add(PCSoundList);
        AbilitySoundList.Add(RCSoundList);
    }

    // (타입, 프리팹) 맵핑
    // T : Enum으로 제한
    private void PrefMap<T>(List<ListGameObject> pList, Dictionary<T, GameObject> pMap) where T : Enum
    {
        // 타입 초기화
        T curType = default;

        for(int i = 0; i < pList.Count; i++)
        {
            for(int j = 0; j < pList[i].gameObjectList.Count; j++)
            {
                // 현재 타입에 해당하는 프리팹 맵핑
                pMap.Add(curType, pList[i].gameObjectList[j]);
                
                // 다음 타입
                curType = (T)(object)(((int)(object)curType) + 1);
            }
        }
    }

    // (타입, 큐) 맵핑
    private void QueMap<T>(Dictionary<T, Queue<GameObject> > qMap, Dictionary<T, GameObject> pMap, int cnt = 50) where T : Enum
    {   
        // 타입을 하나씩 꺼내서
        foreach(T type in Enum.GetValues(typeof(T)))
        {
            // 타입에 해당하는 프리팹을 하나씩 꺼내서
            Queue<GameObject> queue = new Queue<GameObject>();
            GameObject prefab = pMap[type];

            // cnt 개 생성하고 비활성화 후 큐에 저장
            for(int i = 0; i < cnt; i++)
            {
                // 프리팹 생성
                GameObject obj = Instantiate(prefab);

                // 부모를 풀셋으로
                obj.transform.SetParent(poolSet.transform);

                // 비활성화
                obj.SetActive(false);

                // 큐에 저장
                queue.Enqueue(obj);
            }

            // (타입, 큐) 맵핑
            qMap.Add(type, queue);
        }
    }

    // 풀에서 스킬 꺼냄
    public GameObject GetSkill(AbilityType type)
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

    // 풀에서 사운드 꺼냄
    public void GetSound(AbilitySoundType type)
    {
        // 키가 존재하고 큐에 오브젝트가 있으면 꺼냄
        if(queSoundMap.ContainsKey(type) && queSoundMap[type].Count > 0)
        {
            // 오브젝트 꺼내서
            GameObject obj = queSoundMap[type].Dequeue();

            // 오브젝트 활성화
            obj.SetActive(true);
        }
    }

    // 풀에 스킬 반환
    public void ReturnSkill(GameObject obj, AbilityType type)
    {
        // 오브젝트 비활성화
        obj.SetActive(false);

        // 해당 타입의 풀로 반환
        queMap[type].Enqueue(obj);
    }

    // 풀에 사운드 반환
    public void ReturnSound(GameObject obj, AbilitySoundType type)
    {
        // 오브젝트 비활성화
        obj.SetActive(false);

        // 해당 타입의 풀로 반환
        queSoundMap[type].Enqueue(obj);
    }
}
