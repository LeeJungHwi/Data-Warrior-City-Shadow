public static class AbilityEnum
{
    // 스킬 상태
    public enum AbilityState { ready, active, cooldown }

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

    // 무기 타입
    public enum WeaponType { CC, DS, NM, PC, RC }
}
