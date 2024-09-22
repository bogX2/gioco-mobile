using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour
{

    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;
    public  static bool Paused=false;
    public GameObject PausedMenuCanvas;

    public GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale=1f;
         mixer.GetFloat("MyExposedParam",out value);
        volumeSlider.value=value;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            if(Paused){
                Play();
            }
            else{
                Stop();
            }
        }


        if(gameOverUI.activeInHierarchy){
            Cursor.visible=true;
            Cursor.lockState = CursorLockMode.None;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.SetActive(false);
        }
        if(PausedMenuCanvas.activeInHierarchy){
            Cursor.visible=true;
            Cursor.lockState = CursorLockMode.None;
            
        }
    }

    void Stop(){
        PausedMenuCanvas.SetActive(true);
        Time.timeScale=0f;
        Paused=true;
    }

    public void Play(){
       PausedMenuCanvas.SetActive(false);
        Time.timeScale=1f;
        Paused=false;
    }

    public void MainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }


    public void SetVolume(){
        mixer.SetFloat("MyExposedParam",volumeSlider.value);

    }

    public void gameOver(){
        gameOverUI.SetActive(true);
    }

    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   

}
