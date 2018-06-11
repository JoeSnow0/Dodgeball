using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

   
    public float moveSpeed;
    public float bulletSpeed;

    private Rigidbody2D rigidbodyPlayer;
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
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        projectileSpawn.transform.position = new Vector2(0, bulletPrefab.GetComponent<CircleCollider2D>().radius * 2.5f);
    }

    void Update()
    {

        //Movement
        moveHorizontal = Input.GetAxis("MovementHorizontal");
        moveVertical = Input.GetAxis("MovementVertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rigidbodyPlayer.AddForce(movement * moveSpeed, ForceMode2D.Force);
        Mathf.Clamp(rigidbodyPlayer.velocity.magnitude, 0, moveSpeed);
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
        Rigidbody2D newProjectile = Instantiate(bulletPrefab, projectileSpawn.position, projectileSpawn.rotation);
        newProjectile.AddForce(projectileSpawn.position * bulletSpeed, ForceMode2D.Force);
    }
}