using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class video : MonoBehaviour
{
    public GameObject tks;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        double video_time = GetComponent<VideoPlayer>().time;
        Debug.Log(video_time);
        if(video_time >124.5 && !flag){
            flag = true;
            tks.transform.Find("Panel").gameObject.SetActive(true);
        }
        else if(video_time>=158.56){
            tks.transform.Find("Panel").gameObject.SetActive(false);
            SceneManager.LoadScene("Lobby");
        }
        
        
    }
}
