using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float hoverForce = 12.0f;
    public int numero = -1;
    void OnTriggerStay(Collider other){
        string nome = other.name;
        if(nome == "Sphere_"){
            Rigidbody rigid = other.GetComponent<Rigidbody>();
            var pai = rigid.transform.parent;
            numero = pai.GetComponent<Node_>().valor; 
            rigid.AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
        }
        
    }
    // void OnTriggerExit(Collider other){
    //     string nome = other.name;
    //     if(nome == "Sphere_"){
    //         numero = -1; 
    //     }
        
    // }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
