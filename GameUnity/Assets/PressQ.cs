using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PressQ : MonoBehaviour
{
    public GameObject SitWithQ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowQSit(bool flag){
        if(flag) SitWithQ.SetActive(true);
        else SitWithQ.SetActive(false);
    }
}
