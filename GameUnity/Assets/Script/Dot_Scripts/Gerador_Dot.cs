using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador_Dot : MonoBehaviour
{
    public GameObject DotOriginal;
    public bool accepted = false;

    // Start is called before the first frame update
    void Start()
    {
        createDots(10);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.childCount);
        var child_1 = transform.GetChild(0).gameObject;
        accepted = child_1.GetComponent<Dot_Dad>().verifica_sub;                
        if(accepted==true){
            Debug.Log("Accepted!");
        }else{
            Debug.Log("WA!");
        }
        
        
    }

    void createDots(int qtd){
        for(int i=1;i<=qtd;i++){
            int valor = Random.Range(1, 1);
            // tree_nodes.Add(valor);
            GameObject DotClone = Instantiate(DotOriginal, new Vector3(i*2f, DotOriginal.transform.position.y, 0), DotOriginal.transform.rotation);
            DotClone.transform.parent = transform;
            DotClone.SendMessage("getIndex", i,SendMessageOptions.DontRequireReceiver);
            DotClone.SendMessage("getValor", valor,SendMessageOptions.DontRequireReceiver);
        }
    }
}
