using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseControl : MonoBehaviour
{
    public static bool GameIsPaused = false; // Pode acessar essa variável com PauseControl.GameIsPaused em outro código (Como o de musica)
    public GameObject PauseMenu;
    public GameObject GameOver;
    public GameObject Player;
    public GameObject Accepted;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                // Cursor.visible = false;
                Resume();
            }
            else{
                // Cursor.visible = true;
                Pause();
            }
        }
        if(Player.GetComponent<AnimationAndMovementController>().isDead){
            StartCoroutine(routineGameOver());
        }
        if(Player.GetComponent<AnimationAndMovementController>().isAccepted){
            StartCoroutine(routineAccepted());
        }
    }
    IEnumerator routineGameOver(){
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        GameOver.SetActive(true);
    }
    IEnumerator routineAccepted(){
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        Accepted.SetActive(true);
    }
    public void Resume(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void ReturnLobby(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Player.GetComponent<AnimationAndMovementController>().isAccepted = false;
        SceneManager.LoadScene("Lobby");
    }
    void Pause(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    public void Restart(){
        GameOver.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Player.GetComponent<AnimationAndMovementController>().isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Options(){

    }
    public void QuitGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }
}
