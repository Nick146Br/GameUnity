using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// interface ItargetPosition {
//    Vector3 targetPosition { get; set;  }
// }

public class Node_ : MonoBehaviour
{
    public int valor = 0;
    public Vector3 targetPosition = new Vector3(0,0,0);
    float spawnValue = 10f;
    void Start(){

    }
    void Update(){  
        // targetPosition = this.transform.GetChild(0).transform.position;
        // Debug.Log(valor);
       
    }
    void LateUpdate(){
        if(transform.GetChild(0).position.y < -spawnValue){
            transform.GetChild(0).position = new Vector3(0f, 30f, 0f);            
        }  
    }

   
    void getVal(int val){
        valor = val;
        // Debug.Log(valor);
    }

}
