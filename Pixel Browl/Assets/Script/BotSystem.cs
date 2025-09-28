using UnityEngine;
using UnityEngine.AI;

public class BotSystem : MonoBehaviour
{

    public enum BotState {Patrol, Loot, Combat}
    public BotState currentState = BotState.Patrol;

    [SerializeField] Transform player;
    [SerializeField] LayerMask chestMask;
    [SerializeField] LayerMask safeZoneMask;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;

     NavMeshAgent agent;

    [SerializeField] float visionRadius = 8f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float chestDetection = 8f;
    [SerializeField] float fireCooldown = 1f;

    Vector3 patrolTarget;
    [SerializeField] Vector3 offSet = new Vector3(0,0, 18f);


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

    }
}
