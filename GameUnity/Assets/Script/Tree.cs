using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float hoverForce = 12.0f;

    void OnTriggerStay(Collider other){
        string nome = other.name;
        if(nome == "Sphere_"){
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
        }
        
    }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
