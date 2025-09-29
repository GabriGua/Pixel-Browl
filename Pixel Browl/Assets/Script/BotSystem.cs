using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BotSystem : MonoBehaviour
{

    public enum BotState {Patrol, Loot, Combat, Flee}
    public BotState currentState = BotState.Patrol;

    GameObject healthbar;
    Image fillHealth;

    [SerializeField] LayerMask chestMask;
    [SerializeField] LayerMask safeZoneMask;
    [SerializeField] LayerMask targetMask;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;

     NavMeshAgent agent;

    [SerializeField] float visionRadius = 8f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float chestDetection = 8f;
    [SerializeField] float fireCooldown = 1f;

    public float currentHealth = 1000f, maxHealth = 1000f;

    float nextFireTime = 0f;
    float fleeDistance = 6f;

    Vector3 patrolTarget;
    Transform currentTarget;
    [SerializeField] Vector3 offSet = new Vector3(0,0, 18f);
    Vector3 healthOffSet = new Vector3(0, 1f, 0);
    float countDown = 0, regenTime = 3, regenCT = 0;
    float regenRate = 50f;
    int stormCountDown = 1;
    float damage = 50f;

    bool canDamage = false;

    GameManager gameManager;


    private void Awake()
    {
        PickRandomDestination();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case BotState.Patrol:
                PatrolBehaviour();
                
                break;
            case BotState.Loot:
                LootBehaviour();
                break;
            case BotState.Combat:
                CombatBehaviour();
                break;
            case BotState.Flee:
                FleeBehaviour();
                break;
        }

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        else
        {
            if (regenCT > 0)
            {

                regenCT -= Time.deltaTime;
            }
            else
            {
                RestoreHealth();
                regenCT = 1;
            }
        }

        if(canDamage)
        {
            if (stormCountDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                currentHealth -= damage;
                stormCountDown = 1;
            }
        }

        fillHealth.fillAmount = currentHealth / maxHealth;
        healthbar.transform.position = gameObject.transform.position + healthOffSet;

    }


    void RestoreHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += regenRate;
        }



    }

    void PatrolBehaviour()
    {
        if (Vector2.Distance(transform.position, patrolTarget) < 0.5f)
        {
            PickRandomDestination();
            
        }
        else
        {
            agent.SetDestination(patrolTarget);
            RotateTowards(patrolTarget);

        }

        Transform target = GetClosestTarget();
        currentState = BotState.Combat;

    }

    void PickRandomDestination()
    {
        for(int i = 0; i < 15; i++)
        {
            patrolTarget = (Vector2)transform.position + Random.insideUnitCircle * 5f;
            patrolTarget += offSet;

            Collider2D zone = Physics2D.OverlapPoint(patrolTarget, safeZoneMask);
            if (zone == null)
            {
                continue;
            }

            NavMeshHit hit;
            if (NavMesh.SamplePosition(patrolTarget, out hit, 2f, NavMesh.AllAreas))
            {
                patrolTarget = hit.position;
                return;

            }
        }
        
        
        
    }

    void RotateTowards(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    void LootBehaviour()
    {

    }

    void CombatBehaviour()
    {
        currentTarget = GetClosestTarget();

        if(currentTarget == null)
        {
            currentState = BotState.Patrol;
            return;
        }

        float dist = Vector2.Distance(transform.position, currentTarget.position);

        float stoppingDistance = attackRange * 0.8f;

        if (stoppingDistance < dist)
        {
            agent.SetDestination(currentTarget.position);
            RotateTowards(currentTarget.position);


        }
        else
        {
            agent.ResetPath();
            RotateTowards(currentTarget.position);

        }

        if (dist < attackRange && Time.time > nextFireTime)
        {
            Shoot();
            fireCooldown = Random.Range(0.6f, 1f);
            nextFireTime = Time.time + fireCooldown;
        }

        if (currentTarget.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth playerHealth = currentTarget.GetComponent<PlayerHealth>();

            if (playerHealth != null && currentHealth < (maxHealth / 2) && currentHealth < playerHealth.health)
            {
                currentState = BotState.Flee;
                return;
            }

        }
        else
        {
            BotSystem botSystem = currentTarget.GetComponent<BotSystem>();
            if (botSystem != null && currentHealth < (maxHealth / 2) && currentHealth < botSystem.currentHealth)
            {
                currentState = BotState.Flee;
                return;
            }
        }
        



    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);

    }

    void FleeBehaviour()
    {
        if (currentTarget == null) return;

        Vector3 fleeDir = (transform.position - currentTarget.position).normalized;
        Vector3 fleePos = transform.position + fleeDir * fleeDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleePos, out hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            RotateTowards(hit.position);

        }

        if (currentTarget.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth playerHealth = currentTarget.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                if (!(currentHealth < (maxHealth / 2) && currentHealth < playerHealth.health))
                {
                    currentState = BotState.Combat;
                    return;
                }
            }

        }
        else
        {
            BotSystem botSystem = currentTarget.GetComponent<BotSystem>();
            if (botSystem != null)
            {
               if (!(currentHealth < (maxHealth / 2) && currentHealth < botSystem.currentHealth))
                {
                    currentState = BotState.Combat;
                    return;
                } 
            }
            
        }
    }

    Transform GetClosestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, visionRadius, targetMask);
        float closestDist = Mathf.Infinity;
        Transform closestTarget = null;


        foreach (Collider2D hit in hits)
        {
            

            if (hit.gameObject == this.gameObject)
                continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);

            if(dist < closestDist)
            {
                closestDist = dist;
                closestTarget  = hit.transform;
            }

            

        }
        
        return closestTarget;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        countDown = regenTime;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
        int j = 0;
    public void StartStormDamage()
    {
        canDamage = true;
    }

    public void StopStormDamage()
    {
        canDamage = false;
    }

    void Die()
    {
        gameManager.BotDied();
        healthbar.SetActive(false);
        Destroy(gameObject);
    }

    public void SetHealthBar(GameObject healthbar)
    {
        this.healthbar = healthbar;
        fillHealth = healthbar.GetComponentInChildren<Image>();
        fillHealth.fillAmount = currentHealth;
    }
}
