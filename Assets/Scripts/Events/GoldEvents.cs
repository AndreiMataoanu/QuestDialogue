using System;
using UnityEngine;

public class GoldEvents
{
    public event Action<int> onGoldGained;

    public void GoldGained(int gold)
    {
        if(onGoldGained != null)
        {
            onGoldGained(gold);
        }
    }
}
