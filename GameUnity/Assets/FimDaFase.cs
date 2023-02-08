using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimDaFase : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject GOPlayer;
    void Start()
    {
        
    }

    void OnTriggerStay(Collider other){
        string nome = other.name;
        Debug.Log(true);
        if(nome == "Jammo_LowPoly"){
            GOPlayer = other.gameObject;
            GOPlayer.GetComponent<AnimationAndMovementController>().isAccepted = true;
        }   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
