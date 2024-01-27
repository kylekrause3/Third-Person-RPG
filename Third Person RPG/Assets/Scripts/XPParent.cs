using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XPParent : MonoBehaviour //PlayerXP, EnemyXP
{
    public enum XPType
    {
        Attack,
        Ranged,
        Magic
    }

    public Dictionary<XPType, int> xp = new Dictionary<XPType, int>();
    public Dictionary<XPType, int> xpLevel = new Dictionary<XPType, int>();

    [Header("Parent Vars")]
    public List<int> xpPerLevel;
    public List<float> accuracyPerLevel;

    public int maxLevel = 100;
    public float exponent = 2.5f;

    public float startingAccuracy;
    public float halfwayAccuracy;


    //private stuff for accuracy
    private float a;
    private float f1;
    private float f2;

    private float p11;
    private float p13;
    private float p2;

    //other private parts ;)
    private Camera mainCamera;
    private Transform hitSplatParent;


    private void Awake()
    {
        //Accuracy constants
        a = (1f - (2f * halfwayAccuracy)) / (halfwayAccuracy - 1f);
        f1 = 1f / (1f - (1f / (a + 1f)));
        f2 = 1f + 1f / ((1f / startingAccuracy) - 1f);
        p11 = -1f * a * f2;
        p13 = 1 / a;
        p2 = 1 - startingAccuracy;

        //Level stuff
        foreach (XPType x in Enum.GetValues(typeof(XPType)))
        {
            xp.Add(x, 1);
            xpLevel.Add(x, 1);
        }
        for (int i = 0; i <= maxLevel; i++)
        {
            xpPerLevel.Add((int)Mathf.Pow(i, exponent)); //LEVEL 100 = 1000xp with x^1.5, 10000 with x^2
            accuracyPerLevel.Add(calculateAccuracy(i));
        }
    }

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hitSplatParent = GameObject.Find("hitSplats").GetComponent<Transform>();
    }

    public virtual void addXP(XPType type, int add)
    {

        xp[type] += add;
        //Transform pfHitSplat = Resources.Load<Transform>("hitSplat");
        Transform pfXpSplat = Resources.Load<Transform>("xpSplat");
        //public static void Create(Camera cam, Transform parent, Transform recipient, int damageAmount, Transform pfImport)
        //hitSplat.Create(mainCamera, hitSplatParent, target, damage);

        
        hitSplat.Create(mainCamera, hitSplatParent, this.transform, add, pfXpSplat, 2f);
        if (xp[type] >= xpPerLevel[xpLevel[type] + 1] && xpLevel[type] < maxLevel)
        {
            xpLevel[type] += 1;
        }
    }

    public float getXP(XPType type)
    {
        return xp[type];
    }

    public float getLevel(XPType type)
    {
        return xpLevel[type];
    }

    public float xpToNextLevel(XPType type)
    {
        if (xpLevel[type] < 100)
        {
            return xpPerLevel[xpLevel[type] + 1] - xpLevel[type];
        }
        else
        {
            return 0f;
        }
    }

    private float calculateAccuracy(int i)
    {
        float p12 = (float)i / (float)maxLevel;
        float p1 = 1 / (p11 * (p12 + p13));
        return startingAccuracy + f1 * (p1 + p2);
    }

    public void fixLevels()
    {
        foreach(XPType x in Enum.GetValues(typeof(XPType)))
        {
            xpLevel[x] = (int)Mathf.Pow(xp[x] + 1, 1 / exponent);
        }
    }

}


