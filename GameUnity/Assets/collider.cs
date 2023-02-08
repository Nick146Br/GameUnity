using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider col;
    void Start()
    {
        col = GetComponent<BoxCollider>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
