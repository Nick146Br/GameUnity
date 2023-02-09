using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingletonExample.Instance.Score_tree = 0;
        SingletonExample.Instance.Score_jos = 0;
        SingletonExample.Instance.Score_fib = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
