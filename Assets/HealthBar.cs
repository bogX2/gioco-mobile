using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
public TMP_Text healthBarText;
public Slider healthSlider;
    Damageable playerDamageable;
    // Start is called before the first frame update

    private void Awake(){
        Thread.Sleep(2000);
     GameObject player = GameObject.FindGameObjectWithTag("Player");
     if(player==null){
        Debug.Log("player not found in the scene");
     }
     playerDamageable= player.GetComponent<Damageable>();
    }


    void Start()
    {


     healthSlider.value=CalculateSliderPercentage(playerDamageable.Health,playerDamageable.MaxHealth);
     healthBarText.text="HP" + playerDamageable.Health +"/"+ playerDamageable.MaxHealth;


    }

    private void OnEnable(){

        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
        

    }


    private void OnDisable(){
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(int currentHealth,int MaxHealth)
    {
        throw new NotImplementedException();
    }


    private void OnPlayerHealthChanged(int newHealth, int maxHealth){

        healthSlider.value=CalculateSliderPercentage(newHealth,maxHealth);
     healthBarText.text="HP" + newHealth +"/"+ maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
