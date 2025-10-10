using System.Collections.Generic;
using UnityEngine;

public class YieldReturnManager : Singleton<YieldReturnManager>
{
    public readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    public readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    Dictionary<float, WaitForSeconds> dic = new Dictionary<float, WaitForSeconds>();
    WaitForSeconds wait;

    public WaitForSeconds WaitForSecond(float num)
    {
        if (dic.TryGetValue(num, out wait))
            return wait;
        else
        {
            dic.Add(num, new WaitForSeconds(num));
            return dic[num];
        }
    }
}
