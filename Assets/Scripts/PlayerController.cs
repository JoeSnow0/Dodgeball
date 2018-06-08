using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

   
    public float speed;

    private Rigidbody2D rb;
    public Transform playerModel;
    public Rigidbody2D bulletPrefab;
    [SerializeField] Transform projectileSpawn;
    float moveHorizontal;
    float moveVertical;
    float angle;

    public void Reset()
    {
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //Movement
        moveHorizontal = Input.GetAxis("MovementHorizontal");
        moveVertical = Input.GetAxis("MovementVertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.AddRelativeForce(movement * speed, ForceMode2D.Force);
        Mathf.Clamp(rb.velocity.magnitude, 0, speed);
        print(rb.velocity.magnitude);
        //Rotation
        var angH = Input.GetAxis("RotationHorizontal");
        var angV = Input.GetAxis("RotationVertical");

        if (angH != 0.0f || angV != 0.0f)
        {
            angle = Mathf.Atan2(angH, angV) * Mathf.Rad2Deg;
        }
        playerModel.eulerAngles = new Vector3(0, 0, -angle);


        //Shooting
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        Rigidbody2D newProjectile = Instantiate(bulletPrefab, projectileSpawn.position, gameObject.transform.rotation);
    }
}