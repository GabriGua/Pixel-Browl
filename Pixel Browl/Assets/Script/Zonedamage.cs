using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Zonedamage : MonoBehaviour
{


    PlayerHealth health;
    int damage = 10;
    bool canDamage;
    float countDown = 0;

   

    private void Update()
    {
        if (canDamage)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                OutOfStorm();
                countDown = 1;
            }

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                
                canDamage = true;
                countDown = 1;
                
               
            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                
                canDamage = false;
            }
        }

    }

    void OutOfStorm()
    {
        if (health.health > 0)
        {
            health.TakeDamage(damage);

        }
       
    }

    public void SetSafeZoneTarget(PlayerHealth playerHealth)
    {
        health = playerHealth;
    }
}
