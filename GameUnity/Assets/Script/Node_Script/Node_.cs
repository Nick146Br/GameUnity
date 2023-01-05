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

    void Start(){

    }
    void Update(){  
        // targetPosition = this.transform.GetChild(0).transform.position;
        Debug.Log(valor);
        
    }

   
    void getVal(int val){
        valor = val;
        // Debug.Log(valor);
    }

}
