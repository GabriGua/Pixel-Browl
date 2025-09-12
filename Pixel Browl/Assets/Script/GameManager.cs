using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] chestSpawnPoint;
    [SerializeField] private GameObject chest;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnPoint;


    int randomChest, randomSpawn;
    int[] alredySpawned;

    private void Awake()
    {
        randomChest = Random.Range(10, chestSpawnPoint.Length);

        for (int i = 0; i < randomChest; i++)
        {
            
            Instantiate(chest, chestSpawnPoint[i].transform);
        }

        Instantiate(player, spawnPoint.transform);
    }
}
