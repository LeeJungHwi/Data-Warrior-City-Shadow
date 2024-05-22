using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AbilityUserClass
{
    // 게임 오브젝트 형 리스트를 가지는 클래스
    [Serializable]
    public class ListGameObject
    {
        public List<GameObject> gameObjectList;

        // 생성자
        public ListGameObject() {}
        public ListGameObject(List<GameObject> gameObjList) { gameObjectList = gameObjList; }
    }

    // 선택된 무기 스킬 이미지
    [Serializable]
    public class AbilityImage
    {
        public Sprite Q, W, E; // Q W E

        // 생성자
        public AbilityImage(Sprite q, Sprite w, Sprite e)
        {
            Q = q;
            W = w;
            E = e;
        }
    }

    // 선택된 무기 스킬 정보
    [Serializable]
    public class AbilityInfo
    {
        // Q W E 정보
        [Header ("스킬 이름")] public string[] abilityName;
        [Header ("스킬 아이콘")] public Sprite[] abilityIcon;
        [Header ("스킬 설명")] public string[] abilityDesc;

        // 생성자
        public AbilityInfo(string[] name, Sprite[] icon, string[] desc)
        {
            abilityName = name;
            abilityIcon = icon;
            abilityDesc = desc;
        }
    }

    // 선택된 무기 프레임 이미지
    [Serializable]
    public class SelectedFrameImage
    {
        public Image weaponFrame, changeFrame; // 무기프레임, 무기교체프레임

        // 생성자
        public SelectedFrameImage(Image wFrame, Image cFrame)
        {
            weaponFrame = wFrame;
            changeFrame = cFrame;
        }
    }

    // 해제된 무기 체크 및 자물쇠 이미지
    [Serializable]
    public class WeaponUnlockImage
    {
        public GameObject unlockImage; // 자물쇠 이미지
        public bool isUnlock; // 해제된 무기인지 체크

        // 생성자
        public WeaponUnlockImage(GameObject unlockImg, bool isUnlo)
        {
            unlockImage = unlockImg;
            isUnlock = isUnlo;
        }
    }
}
