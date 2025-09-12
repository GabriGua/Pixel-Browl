using UnityEngine;

public class CameraRect : MonoBehaviour
{

    [SerializeField] private GameObject player;
    Vector2 playerPos, playerX, playerY;
    Camera cam;
    [SerializeField] GameObject[] cameraLimit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        playerX = new Vector2(player.transform.position.x, transform.position.y);
        playerY = new Vector2(transform.position.x, player.transform.position.y);

        if (cam != null) 
        {
            if (player.transform.position.x > cameraLimit[0].transform.position.x && player.transform.position.x < cameraLimit[1].transform.position.x && player.transform.position.y > cameraLimit[1].transform.position.y && player.transform.position.y < cameraLimit[0].transform.position.y)
            {
                cam.transform.position = playerPos;
            }
            else if (player.transform.position.y > cameraLimit[1].transform.position.y && player.transform.position.y < cameraLimit[0].transform.position.y)
            {
                cam.transform.position = playerY;
            }
            else if(player.transform.position.x > cameraLimit[0].transform.position.x && player.transform.position.x < cameraLimit[1].transform.position.x)
            {
                cam.transform.position = playerX;
            }
            
        }
        
    }
}
