using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador_Josephus : MonoBehaviour
{
    [SerializeField] public Material PointColor;
    Material OldColor;
    public GameObject Ponto;
    bool allowedToStart = false;
    int ChildThePlayerIs;
    [Header("JosephusAlgorithm")]
    [SerializeField] int JosephusJump = 3;
    int SizeOfCircle; 
    // int timeBetween = 1000;
    // float InicioY = 0.0f;
    // float InicioX = 0.0f;

    // Start is called before the first frame update
    void Start(){
        int num = UnityEngine.Random.Range(4, 10);
        SizeOfCircle = num;
        Create(num);
    }

    // Update is called once per frame
    void Update()
    {
        if(allowedToStart){
            Josephus();
            allowedToStart = false;
        }
    }

    void Josephus(){
        StartCoroutine(Coroutine());
    }
    IEnumerator Coroutine(){
        bool flagInterna = false;
        Transform t = transform.GetChild(0);;
        int pos = 0;
        int tot = SizeOfCircle;
        for(int i = 1; i < SizeOfCircle; i++){
            pos--;
            for(int j = 1; j <= JosephusJump; j++){
                pos++;
                pos %= tot;
                
                t = transform.GetChild(pos);
                MeshRenderer meshRenderer = t.GetChild(2).GetComponent<MeshRenderer>();
                meshRenderer.enabled = true;
                OldColor = meshRenderer.material;
                if(j == JosephusJump){
                    yield return new WaitForSeconds(1);
                    meshRenderer.material = PointColor;
                    yield return new WaitForSeconds(1);
                }
                else yield return new WaitForSeconds(1);
                
                meshRenderer.enabled = false;
                meshRenderer.material = OldColor;
            }
            if(ChildThePlayerIs == pos){
                transform.GetChild(ChildThePlayerIs).GetChild(0).GetChild(1).gameObject.SendMessage("allowToStand", false, SendMessageOptions.DontRequireReceiver);
                flagInterna = true;
                break;
            }
            if(ChildThePlayerIs > pos)ChildThePlayerIs--;
            t.GetChild(0).GetChild(1).gameObject.SendMessage("Deactivate", true, SendMessageOptions.DontRequireReceiver);
            t.parent = null;
            tot--;
        }
        if(!flagInterna)transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SendMessage("allowToStand", true, SendMessageOptions.DontRequireReceiver);
        
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
    void Begin(List<int> arr){
        
        allowedToStart = (arr[0] == 1) ? true : false;
        
        ChildThePlayerIs = arr[1];
        Debug.Log(ChildThePlayerIs);
    }
}
