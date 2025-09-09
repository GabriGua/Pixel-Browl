using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    float health = 100;
    bool damageTaken = false;
    [SerializeField] Image healthbar;
    int k = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthbar.fillAmount = health;
       
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = health/100;

        if (damageTaken == false)
        {
            if(k == 0 && health > 0)
            {StartCoroutine(RestoreHealth());
                k++;
            
            }
            

        }
        else
        {
            if (k > 0)
            {
                StopCoroutine(RestoreHealth());
                k = 0;
            }
        }
       
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            damageTaken = true;
            
            StartCoroutine(countDown());

        }
    }

    IEnumerator countDown()
    {
        yield return new WaitForSeconds(3);

        damageTaken = false;
        
    }

    IEnumerator RestoreHealth()
    {

        while (health < 100 && damageTaken == false)
        {
            yield return new WaitForSeconds(1);
            health += 10;

            
           
        }

    }
}
