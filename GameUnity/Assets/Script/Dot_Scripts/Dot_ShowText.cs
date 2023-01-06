using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot_ShowText : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    private int index =0;
    private int valor=0;
    private bool verifica_sub;
    private float height = -0.5f;
    // Start is called before the first frame update
    void Start()
    {
        var dad = gameObject.GetComponentInParent<Dot_Dad>();
        index = dad.index;
        valor = dad.valor;
        verifica_sub = dad.verifica_sub;
        Vector3 target_position = dad.transform.GetChild(0).transform.position + new Vector3(0,height,0);
        transform.position = target_position;
        if(FloatingTextPrefab){
            ShowFloatingText();
        }
    }
    void Update()
    {
        
    }

    // Update is called once per frame
    void ShowFloatingText(){
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity,transform);
        go.GetComponent<TMPro.TextMeshPro>().text = index.ToString() + '\n' + valor.ToString() + '\n' + verifica_sub.ToString();
        // go.GetComponent<TMPro.TextMeshPro>().alignment = AlignmentTypes.Center;
    }
}
