using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    bool Segurada = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if(Segurada){
            transform.root.gameObject.SendMessage("Colisao", true, SendMessageOptions.DontRequireReceiver);
        }    
    }
    void OnCollisionExit(Collision other) {
        if(Segurada){
            transform.root.gameObject.SendMessage("Colisao", false, SendMessageOptions.DontRequireReceiver);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SendoSegurada(bool flag){
        Segurada = flag;
    }
}
