using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    float health = 100;
    bool damageTaken = false;
    [SerializeField] Image healthbar;
    int k = 0, hit = 0 ;
    int hits;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthbar.fillAmount = health;
       
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = health/100;

        Debug.Log(damageTaken);

        if (damageTaken == false)
        {
            if(k == 0 && health > 0)
            {k++;
                StartCoroutine(RestoreHealth());
                
                
            
            }
            

        }
        else
        {
            if (k > 0)
            {k = 0;
                StopCoroutine(RestoreHealth());
                
            }
        }
       
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;


            
            damageTaken = true;

            hit++;
        }

        if (hits != hit)
        {
            StartCoroutine(countDown());
            hits  = hit;
        }
    }

    IEnumerator countDown()
    {
       
        yield return new WaitForSeconds(3);

        while(hits != hit)
        {
            hits = hit;
            yield return new WaitForSeconds(3);
            Debug.Log("wait");
            
        }
        Debug.Log("stop");
        damageTaken = false;
        
    }

    IEnumerator RestoreHealth()
    {

        while (health < 100 && damageTaken == false)
        {
            yield return new WaitForSeconds(1);
            health += 10;

            
           
        }

        if (health >99)
        {
            damageTaken = true;
        }


    }
}
