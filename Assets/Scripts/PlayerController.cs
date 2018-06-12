﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

   
    public float moveSpeed;
    public float bulletSpeed;
    private int playerNumber { get; set; }
    [SerializeField] private int playerAmmo { get; set; }

    private Rigidbody2D rigidbodyPlayer;
    public Transform playerModel;
    public Rigidbody2D bulletPrefab;
    [SerializeField] Transform projectileSpawn;
    float moveHorizontal;
    float moveVertical;
    float angle;
    float ballSpawnOffset = 0.2f;

    public void Reset()
    {
        
    }

    void Start()
    {
        playerAmmo = 1;
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        projectileSpawn.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + bulletPrefab.GetComponent<CircleCollider2D>().radius * 2f + ballSpawnOffset);
    }

    void Update()
    {

        //Movement
        PlayerMovement();
        //Rotation
        PlayerRotation();

        //Shooting
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        if (playerAmmo < 1)
            return;

        Vector3 direction = gameObject.transform.position - projectileSpawn.position;
        Rigidbody2D newProjectile = Instantiate(bulletPrefab, projectileSpawn.position, projectileSpawn.rotation);
        newProjectile.AddForce((direction) * -bulletSpeed * moveSpeed, ForceMode2D.Impulse);
        newProjectile.gameObject.GetComponent<BulletController>().myController.ballNumber = playerNumber;
        playerAmmo--;
    }

    private void PlayerRotation()
    {
        var angH = Input.GetAxis("RotationHorizontal");
        var angV = Input.GetAxis("RotationVertical");

        if (angH != 0.0f || angV != 0.0f)
        {
            angle = Mathf.Atan2(angH, angV) * Mathf.Rad2Deg;
        }
        playerModel.eulerAngles = new Vector3(0, 0, -angle);
    }
    private void PlayerMovement()
    {
        moveHorizontal = Input.GetAxis("MovementHorizontal");
        moveVertical = Input.GetAxis("MovementVertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rigidbodyPlayer.AddForce(movement * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        Mathf.Clamp(rigidbodyPlayer.velocity.magnitude, 0, moveSpeed);
        print(rigidbodyPlayer.velocity.magnitude);

    }
}