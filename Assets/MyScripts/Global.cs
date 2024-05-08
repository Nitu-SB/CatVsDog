using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public  class Global : MonoBehaviour
{
    public static Global Instance;
    public Global()
    {
        Instance = this;
    }

    public float minDamage=10,maxDamage=25;
    public Dictionary<BelongCamp,Color> colorDict = new Dictionary<BelongCamp,Color>();
    private void Awake()
    {
        colorDict.Add(BelongCamp.Cat, new Color(243f / 255f, 101f / 255f, 74f / 255f, 1));
        colorDict.Add(BelongCamp.Dog, new Color(74f / 255f, 194f / 255f, 243f / 255f, 1));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum BelongCamp { 
    Cat,
    Dog
}
