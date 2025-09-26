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

    [SerializeField] Vector3 patrolTarget;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case BotState.Patrol:
                PatrolBehaviour();
                Debug.Log("Lets patrol");
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
            Debug.Log("Picking");
        }
        else
        {
            agent.SetDestination(patrolTarget);
            Debug.Log("Lets go");
            Debug.Log(agent.name);
        }


    }

    void PickRandomDestination()
    {
        patrolTarget = (Vector2)transform.position + Random.insideUnitCircle * 5f;
        Debug.Log("New pos");
        Debug.Log(patrolTarget);
    }

    void LootBehaviour()
    {

    }

    void CombatBehaviour()
    {

    }
}
