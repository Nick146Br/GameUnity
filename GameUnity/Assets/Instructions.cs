using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Instructions : MonoBehaviour
{

    public Animator transition;
    public GameObject Explanation;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    float transitionTime = 1f;
    void Start()
    {   
        text = Explanation.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        Debug.Log(text);
        Explanation.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            // Text.SetActive(false)
            
            text.enabled = false;
            StartCoroutine(Transaction());
        }
    }
    IEnumerator Transaction()
    {
        // Play animation
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        Explanation.SetActive(false);
        // SceneManager.LoadScene(levelIndex);
    }
}
