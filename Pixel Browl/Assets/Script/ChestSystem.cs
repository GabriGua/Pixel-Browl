using UnityEngine;
using UnityEngine.UI;

public class ChestSystem : MonoBehaviour
{

    Animator animator;
    [SerializeField] Image healthBar;
    float chestHealth = 500f;
    [SerializeField] GameObject powerUp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        powerUp.SetActive(false);
        animator = GetComponent<Animator>();
        healthBar.fillAmount = chestHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = chestHealth/500;
    }

    public void TakeDamage(float damage)
    {
        if (healthBar != null)
        {
            if (chestHealth >= 0)
            {
                chestHealth -= damage;
                
            }
            else
            {
                animator.Play("Opening");
            }
        }
    }

    public void GivePowerUp()
    {
        powerUp.SetActive(true);
        Destroy(gameObject);
    }
}
