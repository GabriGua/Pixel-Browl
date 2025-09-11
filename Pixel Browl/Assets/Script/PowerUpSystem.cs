using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{

    [SerializeField] PlayerHealth PlayerHealth;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {

                PlayerHealth.AddPowerUp();
                    Destroy(gameObject);
                
            }
        }
    }
}
