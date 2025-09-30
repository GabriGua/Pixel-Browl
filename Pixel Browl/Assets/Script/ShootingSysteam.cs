using System.Collections;
using UnityEngine;

public class ShootingSysteam : MonoBehaviour
{

    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private GameObject rotator;
    [SerializeField] private GameObject shootingArea;
    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject player;

   
    

    int bullets = 3;
    [SerializeField] SpriteRenderer render;

    [SerializeField] private Sprite[] bulletStatus;

    public bool canShoot = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        shootingArea.SetActive(false);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            canShoot = true;
            shootingArea.SetActive(true);
            float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            rotator.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {
            shootingArea.SetActive(false);
        }

        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
           
            
            if (canShoot && bullets > 0)
            {
                canShoot = false;
                player.transform.rotation = rotator.transform.rotation;
                
                
                Instantiate(bullet, bulletSpawn.transform);
                
                
                bullets--;
                if (bullets == 2)
                {
                    Debug.Log("reload Started");
                    StartCoroutine(ReloadBullet());
                }
                UpdateSprite();
                
            }
        }
    }

    IEnumerator ReloadBullet()
    {
        while (bullets < 3)
        {
            yield return new WaitForSeconds(1);
            bullets++;
            UpdateSprite();
            
        }
        

    }

    void UpdateSprite()
    {
        if (bullets == 3)
        {
            render.sprite = bulletStatus[0];
        }
        else if (bullets == 2)
        {
            render.sprite = bulletStatus[1];
            

        }
        else if (bullets == 1)
        {
            render.sprite = bulletStatus[2];


        }
        else if (bullets == 0)
        {
            render.sprite = bulletStatus[3];


        }

    }

    public void SetShootingJoystick(FixedJoystick joystickM)
    {
        joystick = joystickM;
    }
}
    
