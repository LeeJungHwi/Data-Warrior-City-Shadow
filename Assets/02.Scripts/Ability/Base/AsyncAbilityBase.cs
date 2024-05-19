using System.Collections;
using UnityEngine;

public class AsyncAbilityBase : AbilityBase
{
    protected WaitForSeconds oneSecond = new WaitForSeconds(1f); // 1초 캐싱
    protected WaitForSeconds eightTenthsSecond = new WaitForSeconds(0.8f); // .8초 캐싱
    protected WaitForSeconds halfSecond = new WaitForSeconds(0.5f); // .5초 캐싱
    protected WaitForSeconds fourTenthsSecond = new WaitForSeconds(0.4f); // .4초 캐싱
    protected WaitForSeconds twoTenthsSecond = new WaitForSeconds(0.2f); // .2초 캐싱

    // 비동기 스킬
    public virtual IEnumerator Cast() { yield break; }
}
