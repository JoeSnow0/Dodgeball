using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public BulletController myController;
    public int ballNumber { get; set; }
    public float moveSpeed;
    int bounceCounter = 0;
    const int bounceMax = 10;
    private Vector2 m_Velocity;
    public Rigidbody2D rigidbodyBullet;

    // Use this for initialization
    void Start()
    {
        myController = this;
        bounceCounter = 0;
        rigidbodyBullet = GetComponent<Rigidbody2D>();
        Vector2 dir = rigidbodyBullet.velocity.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeed();
        CheckBounces();
    }

    Vector2 latestCollPos;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = coll.gameObject.GetComponent<PlayerController>();
            if (playerController.playerNumber != ballNumber)
            {
                playerController.SubtractHealth();
            }
            BallDeath();

        }
        else
        {
            bounceCounter++;
            //
            if (coll.contacts.Length > 0)
            {
                latestCollPos = coll.contacts[0].point;
                Bounce(coll.contacts[0].normal);

                Vector2 dir = Vector2.zero;
                foreach (var c in coll.contacts)
                {
                    dir += c.normal;
                }
                dir.Normalize();
            }
            else
            {
                Vector2 dir = rigidbodyBullet.velocity.normalized;
            }

            
        }
    }

    private void OnDrawGizmos()
    {
    }
    private void CheckBounces()
    {
        if (bounceCounter > bounceMax)
        {
            Debug.Log("Bounces at death:" + bounceCounter);
            BallDeath();
        }
    }
    float timer;
    private void CheckSpeed()
    {
        if (rigidbodyBullet.velocity.magnitude < 2)
        {
            timer += Time.deltaTime;
            Debug.Log("Speed at death:" + rigidbodyBullet.velocity.magnitude);
            if (timer > 1)
                BallDeath();
        }
        else
            timer = 0;
    }
    //private void CollisionWithPlayer()
    //{

    //    BallDeath();
    //}

    private void BallDeath()
    {
        //Apply ammo

        //Destroy object
        Destroy(gameObject);
    }

    void Bounce(Vector2 normal)
    {
        Vector2 dir = rigidbodyBullet.velocity.normalized;

        if (Vector2.Dot(dir, normal) > 0)
        {
            //Debug.Log("Same normal direction");
            return;
        }
        SetDirection(Vector2.Reflect(dir, normal));

        //Debug.Log("direction: " + dir + " normal: " + normal + " Reflect: " + Vector2.Reflect(dir, normal));
    }

    public void SetDirection(Vector2 direction)
    {
        rigidbodyBullet.AddForce(direction);
    }
}
