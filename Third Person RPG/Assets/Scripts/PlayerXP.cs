using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerXP : XPParent
{
    public void setXP(XPType type, int set)
    {
        xp[type] = set;
        xpLevel[type] = (int)Mathf.Pow(set, 1 / exponent);
    }

    public void setXPLevel(XPType type, int set)
    {
        xpLevel[type] = set;
        xp[type] = xpPerLevel[xpLevel[type]];
    }

}
