using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     Rigidbody2D _rigidbody2D;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private GameObject player;
    private ShootingSysteam ShootingSysteam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        ShootingSysteam = gameObject.GetComponent<ShootingSysteam>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = new Vector2(joystick.Horizontal * moveSpeed, joystick.Vertical * moveSpeed);


        if ((joystick.Horizontal != 0 || joystick.Vertical != 0)  && ShootingSysteam.canShoot == false)
        {
            float angle = Mathf.Atan2(_rigidbody2D.linearVelocity.y, _rigidbody2D.linearVelocity.x) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        
    }
}
