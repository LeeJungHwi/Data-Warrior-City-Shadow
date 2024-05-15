using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsyncAbilityBase : AbilityBase
{
    // 비동기 스킬
    public virtual IEnumerator Cast() { yield break; }
}
