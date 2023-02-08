using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Phases : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TreePhase(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("TreePhase");
    }

    public void FibonacciPhase(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("FibonacciPhase");
    }

    public void JosephusPhase(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("JosephusPhase");
    }

    public void QuitGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
