using UnityEngine;

public class BulletDestruction : MonoBehaviour
{
  
    PlayerHealth playerHealth;
    ChestSystem chestSystem;
    float damage = 100f, power = 10f;

    private void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (playerHealth.health > 0)
        {
            damage += power * playerHealth.powerUps;
            Debug.Log(damage);
        }
    }

    public void DestroyBullet()
    {
        
        Destroy(gameObject);
    }

    
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "chest")
            {
                chestSystem = collision.gameObject.GetComponent<ChestSystem>();

                chestSystem.TakeDamage(damage);
                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Bot")
            {
                Debug.Log("Bot Colpito");
            }

            if (collision.gameObject.tag == "Player")
            {

            }
        }
    }
}


