using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeradorFibonacci : MonoBehaviour
{
    
    public GameObject PlatformType;
    public Material Correct;
    public Material Wrong;
    public Material Original;
    public GameObject Part2Scenario;
    List<List<int>> M;
    // int timeBetween = 1000;
    // float InicioY = 0.0f;
    // float InicioX = 0.0f;
    List <int> fib  = new List <int> ();
    Dictionary <int, bool> dic = new Dictionary<int, bool> ();
    // Start is called before the first frame update
    void Start(){
        // int num = SizeOfCircle;
        fib.Add(1); fib.Add(1);
        dic.Add(1, true);
        for(int i = 2; i <= 20; i++){
            fib.Add(fib[i-1] + fib[i-2]);
            dic.Add(fib[i], true);
        }
        int lin = UnityEngine.Random.Range(11, 12), col = UnityEngine.Random.Range(4, 7);

        M = new List<List<int>> ();

        for(int i = 1; i <= lin; i++){
            List <int> B = new List<int> ();
            for(int j = 1; j <= col; j++){
                B.Add(-1);
            }
            M.Add(B);
        }
        Create(lin, col);

    }

    // Update is called once per frame
    void Update()
    {
        // if(allowedToStart){
        //     Josephus();
        //     allowedToStart = false;
        // }
    }

    // IEnumerator Coroutine(){
    //     bool flagInterna = false;
    //     Transform t = transform.GetChild(0);;
    //     int pos = 0;
    //     int tot = SizeOfCircle;
    //     for(int i = 1; i < SizeOfCircle; i++){
    //         pos--;
    //         for(int j = 1; j <= JosephusJump; j++){
    //             pos++;
    //             pos %= tot;
                
    //             t = transform.GetChild(pos);
    //             MeshRenderer meshRenderer = t.GetChild(2).GetComponent<MeshRenderer>();
    //             meshRenderer.enabled = true;
    //             OldColor = meshRenderer.material;
    //             if(j == JosephusJump){
    //                 yield return new WaitForSeconds(1);
    //                 meshRenderer.material = PointColor;
    //                 yield return new WaitForSeconds(1);
    //             }
    //             else yield return new WaitForSeconds(1);
                
    //             meshRenderer.enabled = false;
    //             meshRenderer.material = OldColor;
    //         }
    //         if(ChildThePlayerIs == pos){
    //             transform.GetChild(ChildThePlayerIs).GetChild(0).GetChild(1).gameObject.SendMessage("allowToStand", false, SendMessageOptions.DontRequireReceiver);
    //             flagInterna = true;
    //             break;
    //         }
    //         if(ChildThePlayerIs > pos)ChildThePlayerIs--;
    //         t.GetChild(0).GetChild(1).gameObject.SendMessage("Deactivate", true, SendMessageOptions.DontRequireReceiver);
    //         t.parent = null;
    //         tot--;
    //     }
    //     if(!flagInterna)transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SendMessage("allowToStand", true, SendMessageOptions.DontRequireReceiver);
        
    // }
    void Create(int lin, int col){
        // float angulo = 360.0f/(float)num;
        // // Debug.Log(transform.position.y);
        // Vector3 vec = new Vector3(20.0f, 0.0f, 0.0f);
        float posx = 0.0f, posz = 0.0f, kx = 8.0f, kz = 8.0f, startz = -4.0f*(col + 1);
        
        int aleat = UnityEngine.Random.Range(0, col-1);
        M[0][aleat] = 1;
        for(int i = 1; i < lin; i++){
            if(aleat == col-1) aleat = col-2;
            else if(aleat == 0) aleat = 1;
            else aleat = UnityEngine.Random.Range(aleat-1, aleat+2);
            if(aleat<0)aleat = 0;
            if(aleat>col-1)aleat = col-1;
            M[i][aleat] = fib[i];
        }
            
        for(int i = 0; i < lin; i++){
            posz = 8.0f;
            for(int j = 0; j < col; j++){   
                if(M[i][j] == -1)aleat = UnityEngine.Random.Range(2, 90);
                else aleat = M[i][j];
                GameObject newPlatform = Instantiate(PlatformType, new Vector3(posx + 32.0f, PlatformType.transform.position.y, posz + startz), PlatformType.transform.rotation);
                newPlatform.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(aleat.ToString());
                newPlatform.transform.gameObject.SendMessage("SetNumero", (dic.ContainsKey(aleat)) ? true : false, SendMessageOptions.DontRequireReceiver);
                newPlatform.transform.parent = transform;
                posz += kz;
            }
            posx += kx;
        }
        GameObject newPart2 = Instantiate(Part2Scenario, new Vector3(posx -4.0f + 32.0f + 15.0f, Part2Scenario.transform.position.y, 0.0f), Part2Scenario.transform.rotation);
        newPart2.transform.parent = transform;
    }
    // void Begin(List<int> arr){
        
    //     allowedToStart = (arr[0] == 1) ? true : false;
        
    //     ChildThePlayerIs = arr[1];
    //     Debug.Log(ChildThePlayerIs);
    // }
}

