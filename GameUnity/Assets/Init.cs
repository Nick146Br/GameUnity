using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingletonExample.Instance.Score_tree = -1;
        SingletonExample.Instance.Score_jos = -1;
        SingletonExample.Instance.Score_fib = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
