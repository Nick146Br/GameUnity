using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
        fib.Add(0); fib.Add(1); fib.Add(1);
        dic.Add(0, true);
        dic.Add(1, true);
        // dic.Add(1, true);
        for(int i = 3; i <= 60; i++){
            fib.Add(fib[i-1] + fib[i-2]);
            dic.Add(fib[i], true);
        }
        int lin = UnityEngine.Random.Range(15, 18), col = UnityEngine.Random.Range(6, 8);

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

    void Create(int lin, int col){
        // float angulo = 360.0f/(float)num;
        // // Debug.Log(transform.position.y);
        // Vector3 vec = new Vector3(20.0f, 0.0f, 0.0f);
        float posx = -12.0f, posz = 0.0f, kx = 8.0f, kz = 8.0f, startz = -4.0f*(col + 1);
        
        int aleat = UnityEngine.Random.Range(0, col);
        M[0][aleat] = 0;
        for(int i = 1; i < lin; i++){
            if(aleat == col-1) aleat = col-2;
            else if(aleat == 0) aleat = 1;
            else aleat = UnityEngine.Random.Range(aleat-1, aleat+2);
            if(aleat<0)aleat = 0;
            if(aleat>col-1)aleat = col-1;
            M[i][aleat] = fib[i];
        }
        int ant;   
        for(int i = 0; i < lin; i++){
            ant = fib[i];
            posz = 8.0f;
            for(int j = 0; j < col; j++){   
                if(M[i][j] == -1){
                    aleat = UnityEngine.Random.Range(Math.Max(0,ant-4), ant+5);
                }
                else aleat = M[i][j];
                GameObject newPlatform = Instantiate(PlatformType, new Vector3(posx + 32.0f, PlatformType.transform.position.y, posz + startz), PlatformType.transform.rotation);
                newPlatform.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(aleat.ToString());
                newPlatform.transform.gameObject.SendMessage("SetNumero", (aleat == fib[i]) ? true : false, SendMessageOptions.DontRequireReceiver);
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

