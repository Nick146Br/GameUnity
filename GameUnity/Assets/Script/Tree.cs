using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float hoverForce = 12.0f;
    public int numero = -1;
    public int time = 0;
    [SerializeField] public Material newMaterial;
    Material oldMaterial;

    void OnTriggerStay(Collider other){
        string nome = other.name;
        if(nome == "Sphere_"){
            Rigidbody rigid = other.GetComponent<Rigidbody>();
            var pai = rigid.transform.parent;
            numero = pai.GetComponent<Node_>().valor; 
            rigid.AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
            time = 0;
        }
        
    }
    // void OnTriggerExit(Collider other){
    //     string nome = other.name;
    //     if(nome == "Sphere_"){
    //         numero = -1; 
    //     }
        
    // }
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        oldMaterial = meshRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if(numero == -1) meshRenderer.material = oldMaterial;
        else meshRenderer.material = newMaterial;
        if(time == 90) numero = -1;  
        time += 1;

        
    }
}
