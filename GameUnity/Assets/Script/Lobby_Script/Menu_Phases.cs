using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu_Phases : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if(SingletonExample.Instance.Score_tree != -1){
            GameObject Pontuacao = transform.Find("Tree_Phase").Find("Pontuacao").gameObject;
            Pontuacao.SetActive(true);
            Pontuacao.GetComponent<TextMeshProUGUI>().SetText(SingletonExample.Instance.Score_tree.ToString());
        }
        if(SingletonExample.Instance.Score_fib != -1){
            GameObject Pontuacao = transform.Find("Fibonacci_Phase").Find("Pontuacao").gameObject;
            Pontuacao.SetActive(true);
            Pontuacao.GetComponent<TextMeshProUGUI>().SetText(SingletonExample.Instance.Score_fib.ToString());
        }
        if(SingletonExample.Instance.Score_jos != -1){
            GameObject Pontuacao = transform.Find("Josephus_Phase").Find("Pontuacao").gameObject;
            Pontuacao.SetActive(true);
            Pontuacao.GetComponent<TextMeshProUGUI>().SetText(SingletonExample.Instance.Score_jos.ToString());
        }
        // string saida = "Tree Score: " + (SingletonExample.Instance.Score_tree) + "\n"; 
        // Debug.Log(saida);
        // saida = "Fibonacci Score: " + (SingletonExample.Instance.Score_fib) + "\n"; 
        // Debug.Log(saida);
        // saida = "Josephus Score: " + (SingletonExample.Instance.Score_jos) + "\n";
        // Debug.Log(saida);
    }

    public void TreePhase(){
        Time.timeScale = 1f;
        // Cursor.visible = false;
        SceneManager.LoadScene("TreePhase");
    }

    public void FibonacciPhase(){
        Time.timeScale = 1f;
        // Cursor.visible = false;
        SceneManager.LoadScene("FibonacciPhase");
    }

    public void JosephusPhase(){
        Time.timeScale = 1f;
        // Cursor.visible = false;
        SceneManager.LoadScene("JosephusPhase");
    }

    public void Credits(){
        Time.timeScale = 1f;
        // Cursor.visible = false;
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame(){
        Time.timeScale = 1f;
        // Cursor.visible = false;
        SceneManager.LoadScene("Menu");
    }
}
