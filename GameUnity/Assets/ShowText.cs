using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    // private int valor = 0;
    private float height = 2.5f;
    private Vector3 target_position;
    int number = 0;

    // Start is called before the first frame update
    void Start()
    {
        target_position = transform.parent.position + new Vector3(0,height,0);
        if(FloatingTextPrefab){
            ShowFloatingText();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowFloatingText(){
        // gameObject target = this.transform.parent.GetComponent(targetPosition);
        
        // Debug.Log(this.transform.parent);
        var go =Instantiate(FloatingTextPrefab,target_position, Quaternion.identity,transform);
        go.GetComponent<TMPro.TextMeshPro>().text= number.ToString();
        
    }
    void getNumber(int num){
        number = num;
    }
}
