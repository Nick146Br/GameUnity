using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_Dad : MonoBehaviour
{   
    public int index = 0;
    public int valor = 0;
    private bool verifica = false;
    public bool verifica_sub = false;
    public int contador =0;
    public int filhos =0;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        verifica = true;
        var dad = gameObject.transform.root;
        if(dad.childCount > index*2){
            var child_1 = dad.transform.GetChild((index*2)-1);
            Debug.Log("Pai" + index.ToString() +"=" +  child_1.GetComponent<Dot_Dad>().index.ToString());
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.SetPosition(0, child_1.transform.position);
            if(child_1.GetComponent<Dot_Dad>().valor > valor){
                verifica = false;
            }
            if(child_1.GetComponent<Dot_Dad>().verifica_sub == true){
                contador++;
            }
                filhos++;
        }
        if(dad.childCount > index*2 + 1){
            var child_2 = dad.transform.GetChild((index*2)).gameObject;
            Debug.Log("Pai" + index.ToString() +"=" +  child_2.GetComponent<Dot_Dad>().index.ToString());
            lineRenderer.SetVertexCount(3);
            lineRenderer.SetPosition(2, child_2.transform.position);
            if(child_2.GetComponent<Dot_Dad>().valor > valor){
                verifica = false;
            }
            if(child_2.GetComponent<Dot_Dad>().verifica_sub == true){
                contador++;
            }
                filhos++;
        }
        if(contador == filhos || filhos ==0){
            verifica_sub = true;
        }
        
        // if(verifica==true){
        //     transform.position = this.transform.position + new Vector3(0,3f,0);
        // }
        
        
    }

    // Update is called once per frame
    void Update()
    {

        verifica = true;
        filhos = 0;
        contador =0;
        verifica_sub = false;
        var dad = gameObject.transform.root;
        if(dad.childCount > index*2){
            var child_1 = dad.transform.GetChild((index*2)-1);
            // Debug.Log("Pai" + index.ToString() +"=" +  child_1.GetComponent<Dot_Dad>().index.ToString());
            if(child_1.GetComponent<Dot_Dad>().valor > valor){
                verifica = false;
            }
            if(child_1.GetComponent<Dot_Dad>().verifica_sub == true){
                contador++;
            }
                filhos++;
        }
        if(dad.childCount > index*2 + 1){
            var child_2 = dad.transform.GetChild((index*2)).gameObject;
            // Debug.Log("Pai" + index.ToString() +"=" +  child_2.GetComponent<Dot_Dad>().index.ToString());
            if(child_2.GetComponent<Dot_Dad>().valor > valor){
                verifica = false;
            }
            if(child_2.GetComponent<Dot_Dad>().verifica_sub == true){
                contador++;
            }
                filhos++;
        }
        if(contador == filhos || filhos ==0){
            verifica_sub = true;
        }else{
            verifica_sub = false;
        }
    }

    void getIndex(int val){
        index = val;
    }

    void getValor(int val){
        valor = val;
       
    }
}
