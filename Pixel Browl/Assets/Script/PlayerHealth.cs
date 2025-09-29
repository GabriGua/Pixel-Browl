using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float health = 1000f,maxHealth = 1000f;
        float regenRate = 50f;
   public int powerUps = 0;
    
   float countDown = 0, regenTime = 3, regenCT = 0;

   Image healthbar;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthbar.fillAmount = health;
        Debug.Log(health);
       
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = health/maxHealth;

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        else
        {
            if(regenCT > 0)
            {

                regenCT -= Time.deltaTime;
            }
            else
            {
                RestoreHealth();
                regenCT = 1;
            }
        }

        
        

       
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;



            countDown = regenTime;


        }
        else
        {
            SceneManager.LoadScene(2);
        }

        
    }


    void RestoreHealth()
    {
        if(health < maxHealth)
        {
            health += regenRate;
        }
         


    }

    public void AddPowerUp()
    {
        powerUps++;
        maxHealth += regenRate * powerUps;
    }

    public void SetHealthBar(Image healthbar)
    {
        this.healthbar = healthbar;
    }
}
