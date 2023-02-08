using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonExample : MonoBehaviour
{
    private static SingletonExample _instance;
 
    public static SingletonExample Instance
    {
        get { return _instance; }
    }
 
    void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
            Object.Destroy(this);
    }
 
    public int Score_1;
    public int Score_2;
    public int Score_3;
}
