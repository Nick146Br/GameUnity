using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gerador_Josephus : MonoBehaviour
{
    [SerializeField] public Material PointColor;
    Material OldColor;
    public GameObject Ponto;
    public GameObject Enemy;
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
        Vector3 vec = new Vector3(20.0f, 0.0f, 0.0f);
        Quaternion rotac = Quaternion.Euler(0f, 270f, 0f); 
        float angulo = 360.0f/(float)SizeOfCircle;
        for(int i = 1; i <= SizeOfCircle; i++){
            if(ChildThePlayerIs == i-1){
                vec = Quaternion.Euler(0, angulo, 0) * vec;
                rotac = Quaternion.Euler(0, angulo, 0) * rotac;
                continue;
            }
            GameObject DotClone = Instantiate(Enemy, vec, rotac);
            // Debug.Log(DotClone.transform.position.z);
            DotClone.GetComponent<AnimationAndMovementController>().animator.SetBool("isSitting", true);
            DotClone.GetComponent<AnimationAndMovementController>().isSitting = true;
            DotClone.transform.parent = transform.GetChild(i-1);
            vec = Quaternion.Euler(0, angulo, 0) * vec;
            rotac = Quaternion.Euler(0, angulo, 0) * rotac;
            // DotClone.SendMessage("getIndex", i,SendMessageOptions.DontRequireReceiver);
        }
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
                if(pos == tot)pos %= tot;
                
                t = transform.GetChild(pos);
                MeshRenderer meshRenderer = t.GetChild(2).GetComponent<MeshRenderer>();
                meshRenderer.enabled = true;
                OldColor = meshRenderer.material;
                if(j == JosephusJump){
                    yield return new WaitForSeconds(1);
                    meshRenderer.material = PointColor;
                    if(pos != ChildThePlayerIs){
                        t.Find("Enemy(Clone)").GetComponent<AnimationAndMovementController>().animator.SetBool("isDead", true);
                        // t.GetChild(4).GetComponent<Collider>().animator.SetBool("isDead", true);
                    }
                    else{
                        transform.GetChild(ChildThePlayerIs).GetChild(0).GetChild(1).gameObject.SendMessage("allowToStand", false, SendMessageOptions.DontRequireReceiver);
                        flagInterna = true;
                        break;
                    }
                    yield return new WaitForSeconds(1);
                }
                else yield return new WaitForSeconds(1);
                
                meshRenderer.enabled = false;
                meshRenderer.material = OldColor;
            }
            if(flagInterna)break;
            if(ChildThePlayerIs > pos)ChildThePlayerIs--;
            t.GetChild(0).GetChild(1).gameObject.SendMessage("Deactivate", true, SendMessageOptions.DontRequireReceiver);
            t.parent = null;
            tot--;
        }
        if(!flagInterna){
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SendMessage("allowToStand", true, SendMessageOptions.DontRequireReceiver);
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SendMessage("Accepted", true, SendMessageOptions.DontRequireReceiver);
        }
        
    }
    void Create(int num){
        Quaternion rotac = Quaternion.Euler(0f, 90f, 0f); 
        float angulo = 360.0f/(float)num;
        // Debug.Log(transform.position.y);
        Vector3 vec = new Vector3(20.0f, 0.0f, 0.0f);

        for(int i = 1; i <= num; i++){
            GameObject DotClone = Instantiate(Ponto, vec, rotac);
            // Debug.Log(DotClone.transform.position.z);
            DotClone.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().SetText(i.ToString());
            DotClone.transform.Find("ShowText").gameObject.SendMessage("getNumber", i,SendMessageOptions.DontRequireReceiver);
            DotClone.transform.parent = transform;
            vec = Quaternion.Euler(0, angulo, 0) * vec;
            rotac = Quaternion.Euler(0, angulo, 0) * rotac;
            // DotClone.SendMessage("getIndex", i,SendMessageOptions.DontRequireReceiver);
        }
    }
    void Begin(List<int> arr){
        
        allowedToStart = (arr[0] == 1) ? true : false;
        
        ChildThePlayerIs = arr[1];
        Debug.Log(ChildThePlayerIs);
    }
}
