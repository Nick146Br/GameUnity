using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador : MonoBehaviour
{
    public GameObject SphereOriginal;
    // Start is called before the first frame update
    void Start()
    {
        CreateMarbles(3);
        // GameObject SphereClone = Instantiate(SphereOriginal);
    }

    
    void CreateMarbles(int qtd)
    {
        for(int i=0;i<qtd;i++){
            GameObject SphereClone = Instantiate(SphereOriginal, new Vector3(i*0.6f, SphereOriginal.transform.position.y, i*0.75f), SphereOriginal.transform.rotation);
        }
    }
}
