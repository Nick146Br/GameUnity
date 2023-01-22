using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador_Josephus : MonoBehaviour
{
    public GameObject Ponto;
    // float InicioY = 0.0f;
    // float InicioX = 0.0f;

    // Start is called before the first frame update
    void Start(){
        int num = 10;
        Create(num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create(int num){
        float angulo = 360.0f/(float)num;
        // Debug.Log(transform.position.y);
        Vector3 vec = new Vector3(20.0f, 0.0f, 0.0f);

        for(int i = 1; i <= num; i++){
            GameObject DotClone = Instantiate(Ponto, vec, Quaternion.Euler(0, 0, 0));
            // Debug.Log(DotClone.transform.position.z);

            DotClone.transform.parent = transform;
            vec = Quaternion.Euler(0, angulo, 0) * vec;
            // DotClone.SendMessage("getIndex", i,SendMessageOptions.DontRequireReceiver);
        }
    }
}
