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
    // Start is called before the first frame update
    void Start()
    {
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

}
