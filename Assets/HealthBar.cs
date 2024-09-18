using System;
using System.Collections;
using System.Collections.Generic;
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
     GameObject player= GameObject.FindGameObjectWithTag("Player");
     playerDamageable= player.GetComponent<Damageable>();
    }


    void Start()
    {

     
     healthSlider.value=CalculateSliderPercentage(playerDamageable.Health,playerDamageable.MaxHealth);
     healthBarText.text="HP" + playerDamageable.Health +"/"+ playerDamageable.MaxHealth;


    }

    private void OnEnable(){
        

    }

    private float CalculateSliderPercentage(int currentHealth,int MaxHealth)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
