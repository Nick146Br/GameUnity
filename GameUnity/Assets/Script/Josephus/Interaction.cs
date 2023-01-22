using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float hoverForce = 12.0f;
    public int numero = -1;
    public int time = 0;
    [SerializeField] public Material newMaterial1;
    [SerializeField] public Material newMaterial2;
    [SerializeField] public Material DeactivateMaterial1;
    [SerializeField] public Material DeactivateMaterial2;
    GameObject GOPlayer; 
    Material oldMaterial;
    // bool Dentro = false;
    bool sitHere = false;
    bool isInside = false;
    bool isDeactivate = false;

    void OnTriggerStay(Collider other){
        string nome = other.name;
        if(nome == "Jammo_LowPoly"){
            GOPlayer = other.gameObject;
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
        if(!isDeactivate){
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if(sitHere){
                meshRenderer.material = newMaterial2;
            }
            else{
                if(!isInside) meshRenderer.material = oldMaterial;
                else meshRenderer.material = newMaterial1;
            }
        }

        
    }

    void SitHere(bool flag){
        sitHere = flag;
        List<int> arr = new List<int>();
        arr.Add(1);
        int index = transform.parent.parent.GetSiblingIndex();
        arr.Add(index);
        if(flag)transform.root.gameObject.SendMessage("Begin", arr, SendMessageOptions.DontRequireReceiver);
        // Debug.Log(sitHere);
    }
    void IsInside(bool flag){
        isInside = flag;
    }
    void allowToStand(bool flag){
        // Debug.Log(GOPlayer);
        if(!flag) GOPlayer.SendMessage("Death", true, SendMessageOptions.DontRequireReceiver);
        else GOPlayer.SendMessage("StandUp", true, SendMessageOptions.DontRequireReceiver);
    }
    void Deactivate(bool flag){
        isDeactivate = flag;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = DeactivateMaterial1;
        MeshRenderer m = transform.parent.GetChild(0).GetComponent<MeshRenderer>();
        m.material = DeactivateMaterial2;

    }
}
