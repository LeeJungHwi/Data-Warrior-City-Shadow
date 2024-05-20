using System;
using System.Collections.Generic;
using UnityEngine;
using static AbilityEnum;
using static AbilityUserClass;

public class AbilityPool : MonoBehaviour
{
    public static AbilityPool instance;
    private void Awake() { instance = this; }
    public GameObject poolSet; // 오브젝트 부모
    [Header ("스킬 맵핑")] [SerializeField] private List<ListGameObject> AbilityList = new List<ListGameObject>(); // 각 스킬 리스트 모두 저장
    [SerializeField] private ListGameObject CCList = new ListGameObject(), DSList = new ListGameObject(), NMList = new ListGameObject(), PCList = new ListGameObject(), RCList = new ListGameObject(); // 각 CC, DS, NM, PC, RC 스킬 리스트
    private Dictionary<AbilityType, GameObject> prefMap = new Dictionary<AbilityType, GameObject>(); // (타입, 프리팹) 맵핑
    public Dictionary<AbilityType, Queue<GameObject> > queMap = new Dictionary<AbilityType, Queue<GameObject> >(); // (타입, 큐) 맵핑
    [Header ("사운드 맵핑")] [SerializeField] private List<ListGameObject> AbilitySoundList = new List<ListGameObject>(); // 각 스킬 사운드 리스트 모두 저장
    [SerializeField] private ListGameObject CCSoundList = new ListGameObject(), DSSoundList = new ListGameObject(), NMSoundList = new ListGameObject(), PCSoundList = new ListGameObject(), RCSoundList = new ListGameObject(); // 각 스킬 사운드 리스트
    private Dictionary<AbilitySoundType, GameObject> prefSoundMap = new Dictionary<AbilitySoundType, GameObject>(); // (타입, 프리팹) 맵핑
    public Dictionary<AbilitySoundType, Queue<GameObject> > queSoundMap = new Dictionary<AbilitySoundType, Queue<GameObject> >(); // (타입, 큐) 맵핑

    private void Start()
    {
        // 리스트 초기화
        ListInit(AbilityList, CCList, DSList, NMList, PCList, RCList);
        ListInit(AbilitySoundList, CCSoundList, DSSoundList, NMSoundList, PCSoundList, RCSoundList);

        // (타입, 프리팹) 맵핑
        PrefMap(AbilityList, prefMap); 
        PrefMap(AbilitySoundList, prefSoundMap);

        // (타입, 큐) 맵핑
        QueMap(queMap, prefMap);
        QueMap(queSoundMap, prefSoundMap);
    }

    // 리스트 초기화
    private void ListInit(List<ListGameObject> allPrefList, params ListGameObject[] lists) { allPrefList.AddRange(lists); }

    // (타입, 프리팹) 맵핑
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

    // 꺼냄
    public GameObject GetPool<T>(Dictionary<T, Queue<GameObject> > qMap, T type) where T : Enum
    {
        // 키가 존재하고 큐에 오브젝트가 있으면 꺼냄
        if(qMap.ContainsKey(type) && qMap[type].Count > 0)
        {
            // 오브젝트 꺼내서
            GameObject obj = qMap[type].Dequeue();

            // 오브젝트 활성화
            obj.SetActive(true);

            // 오브젝트 반환
            return obj;
        }

        // 사용 가능한 오브젝트가 없으면
        return null;
    }

    // 반환
    public void ReturnPool<T>(Dictionary<T, Queue<GameObject> > qMap, GameObject obj, T type) where T : Enum
    {
        // 오브젝트 비활성화
        obj.SetActive(false);

        // 해당 타입의 풀로 반환
        qMap[type].Enqueue(obj);
    }
}
