using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    // Start is called before the first frame update
    public Material Correct;
    public Material Wrong;
    bool isInFib;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){

        MeshRenderer m = transform.GetComponent<MeshRenderer>();
        Debug.Log(other);
        if(isInFib) m.material = Correct;
        else{ 
            m.material = Wrong;
            Rigidbody r = transform.GetComponent<Rigidbody>();
            r.useGravity = true;
        }
    }

    void SetNumero(bool flag){
        Debug.Log(flag);
        isInFib = flag;
    }
}
