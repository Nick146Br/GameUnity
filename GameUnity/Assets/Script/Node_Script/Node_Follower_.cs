using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Follower_ : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    private int valor = 0;
    private float height = 0.5f;
    private Vector3 target_position;
    // Start is called before the first frame update
    void Start()
    {
        var dad = gameObject.GetComponentInParent<Node_>();
        // Debug.Log(dad.targetPosition);
        target_position = dad.transform.GetChild(0).transform.position + new Vector3(0,height,0);
        transform.position = target_position;
        valor = dad.valor;
        
        if(FloatingTextPrefab){
            ShowFloatingText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var dad = gameObject.GetComponentInParent<Node_>();
        // Debug.Log(dad.targetPosition);
        target_position = dad.transform.GetChild(0).transform.position + new Vector3(0,height,0);;
        transform.position = target_position;
        
    }

    void ShowFloatingText(){
        // gameObject target = this.transform.parent.GetComponent(targetPosition);
        
        // Debug.Log(this.transform.parent);
        var go =Instantiate(FloatingTextPrefab,target_position, Quaternion.identity,transform);
        go.GetComponent<TMPro.TextMeshPro>().text= valor.ToString();
        
    }
}
