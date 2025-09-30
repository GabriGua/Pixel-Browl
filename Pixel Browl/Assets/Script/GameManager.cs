using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] chestSpawnPoint;
    [SerializeField] private GameObject[] botHealthBars;
    
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject powerUp;
    

    [SerializeField] Camera mainCamera;
    private CameraRect cam;

     private GameObject player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject botPrefab;
    [SerializeField] private GameObject spawnPoint;
    PlayerMovement playerMovement;
    ShootingSysteam shootingSysteam;
    PlayerHealth playerHealth;
    [SerializeField] private GameObject[] playerSpawnPoint;


    [SerializeField] FixedJoystick movementJoystick;
    [SerializeField] FixedJoystick shootingJoystick;
    [SerializeField] Image healthBar;

    [SerializeField] Zonedamage zoneDamage;
    public static int totPlayers;

    int randomChest, randomSpawn;
    int[] alredySpawned;

    private void Awake()
    {
        totPlayers = 7;
        SpawnPlayer();
        randomChest = Random.Range(10, chestSpawnPoint.Length);

        for (int i = 0; i < randomChest; i++)
        {

            GameObject chestSpawned = Instantiate(chest, chestSpawnPoint[i].transform);
            GameObject upPower = Instantiate(powerUp, chestSpawnPoint[i].transform);
            

            PowerUpSystem upSystem = upPower.GetComponent<PowerUpSystem>();

            upSystem.SetPlayerHealth(playerHealth);
            upPower.SetActive(false);
            ChestSystem chestSystem = chestSpawned.GetComponent<ChestSystem>();
            chestSystem.SetPowerUpChildren(upPower);
        }

        BotSpawn();
    }

    

    private void Start()
    {
        

    }

    private void Update()
    {
        if(totPlayers < 2)
        {
            SceneManager.LoadScene(2);
        }
    }

    void SpawnPlayer()
    {
        int randomPos = Random.Range(0, playerSpawnPoint.Length);

        player = Instantiate(playerPrefab, playerSpawnPoint[randomPos].transform);

        playerMovement = player.GetComponent<PlayerMovement>();
        shootingSysteam = player.GetComponent<ShootingSysteam>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.SetHealthBar(healthBar);
        playerMovement.SetMovementJoystick(movementJoystick);
        shootingSysteam.SetShootingJoystick(shootingJoystick);
        cam = mainCamera.GetComponent<CameraRect>();
        zoneDamage.SetSafeZoneTarget(playerHealth);

        

        cam.SetCameraTarget(player);
     
        
        playerSpawnPoint[randomPos] = null;
    }

    void BotSpawn()
    {
        for (int i = 0; i < playerSpawnPoint.Length; i++)
        {
            if (playerSpawnPoint[i] != null)
            {
                player = Instantiate(botPrefab, playerSpawnPoint[i].transform);
                BotSystem botSystem = player.GetComponent<BotSystem>();
                botSystem.SetHealthBar(botHealthBars[i]);
            }
            else
            {
                botHealthBars[i].SetActive(false);
            }
            
     
        }
    }

    public void BotDied()
    {
        totPlayers--;
    }
}
