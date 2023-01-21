using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float hoverForce = 12.0f;
    public int numero = -1;
    public int time = 0;
    [SerializeField] public Material newMaterial;
    Material oldMaterial;
    bool Dentro = false;

    void OnTriggerStay(Collider other){
        string nome = other.name;
        if(nome == "Jammo_LowPoly"){
            // Rigidbody rigid = other.GetComponent<Rigidbody>();
            // var pai = rigid.transform.parent;
            // numero = pai.GetComponent<Node_>().valor; 
            // rigid.AddForce(Vector3.up * hoverForce, ForceMode.Acceleration);
            Dentro = true;
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
        Dentro = false;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        oldMaterial = meshRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if(!Dentro) meshRenderer.material = oldMaterial;
        else meshRenderer.material = newMaterial;
        if(time == 10) Dentro = false;  
        time += 1;

        
    }
}
