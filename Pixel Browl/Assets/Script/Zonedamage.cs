using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Zonedamage : MonoBehaviour
{


    [SerializeField] PlayerHealth health;
    int damage = 10;
    bool canDamage;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                Debug.Log("Sei fuori zona");
                canDamage = true;
                StartCoroutine(OutOfStorm());
                
                Debug.Log(canDamage);
            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Sei dentro zona");
                canDamage = false;
            }
        }

    }

    IEnumerator OutOfStorm()
    {
        Debug.Log("entrato");
        while (canDamage == true)
        {
            Debug.Log("colpito");
            yield return new WaitForSeconds(1);
            health.TakeDamage(damage);
        }
    }
}
