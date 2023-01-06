using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador : MonoBehaviour
{
    public GameObject SphereOriginal;
    public ArrayList tree_nodes = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        CreateMarbles(3);
        // GameObject SphereClone = Instantiate(SphereOriginal);
    }

    
    void CreateMarbles(int qtd)
    {
        for(int i=0;i<qtd;i++){
            int valor = Random.Range(1, 100);
            tree_nodes.Add(valor);
            GameObject SphereClone = Instantiate(SphereOriginal, new Vector3(i*0.6f, SphereOriginal.transform.position.y, i*0.75f), SphereOriginal.transform.rotation);
            SphereClone.SendMessage("getVal", valor,SendMessageOptions.DontRequireReceiver);
        }
    }
}
