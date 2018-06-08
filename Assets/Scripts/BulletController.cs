using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public CameraHelper cameraHelper { get { return Camera.main.GetComponent<CameraHelper>(); } }
    public SpriteRenderer spriteRenderer { get { return GetComponent<SpriteRenderer>(); } }
    public ScreenShake screenShake { get { return Camera.main.GetComponent<ScreenShake>(); } }
    int owner;
    public float moveSpeed;
    public float speedIncrease;

    public AudioClip bounceSfx;
    public AudioClip dieSfx;
    public AudioClip shootSfx;
    private Vector2 m_Velocity;
    private AudioSource m_AudioSource;
    public Rigidbody2D rigidbody2D;
    //private ConstantForce2D constantForce2D;
    //private float m_MoveSpeed;

    // Use this for initialization
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = transform.up * 0.2f;
        Vector2 dir = rigidbody2D.velocity.normalized;
        Debug.Log("Direction: " + dir);
        //SetPaddle ( true );
    }

    // Update is called once per frame
    void Update()
    {
        //SetDirection
        //transform.position += (Vector3)m_Velocity * Time.deltaTime;
    }

    Vector2 latestCollPos;

    void OnCollisionEnter2D(Collision2D coll)
    {
        //AudioHelper.PlayOneShot(m_AudioSource, bounceSfx, 0.5f, 0.5f);
        if (coll.gameObject.CompareTag("Player"))
        {
            CollisionWithPlayer();
        }
        else
        {
            //
            if (coll.contacts.Length > 0)
            {
                latestCollPos = coll.contacts[0].point;
                Bounce(coll.contacts[0].normal);

                Vector2 dir = Vector2.zero; //rigidbody2D.velocity.normalized;
                foreach (var c in coll.contacts)
                {
                    dir += c.normal;
                }
                dir.Normalize();
                
                //Debug.DrawLine(coll.contacts[0].point, Vector2.Reflect(dir, coll.contacts[0].normal), Color.blue, 5f);

                Debug.Log("Direction: " + dir + " " + "Contact Point: " + coll.contacts[0].point + "Reflection Value: " + Vector2.Reflect(dir, coll.contacts[0].normal));

                Debug.DrawRay(coll.contacts[0].point, Vector2.Reflect(dir, coll.contacts[0].normal), Color.cyan, 5f);
            }
            else
            {
                Vector2 dir = rigidbody2D.velocity.normalized;
                Debug.LogError(coll.gameObject.name);
                Debug.DrawRay(coll.contacts[0].point, Vector2.Reflect(dir, coll.contacts[0].normal), Color.red, 5f);
            }

            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(latestCollPos, Vector3.one / 2);
    }

    private void CollisionWithPlayer()
    {
        Destroy(gameObject);
    }

    void Bounce(Vector2 normal)
    {
        Vector2 dir = rigidbody2D.velocity.normalized;

        if (Vector2.Dot(dir, normal) > 0)
        {
            Debug.Log("Same normal direction");
            return;
        }
        SetDirection(Vector2.Reflect(dir, normal));

        Debug.Log("direction: " + dir + " normal: " + normal + " Reflect: " + Vector2.Reflect(dir, normal));
    }

    public void SetDirection(Vector2 direction)
    {
        rigidbody2D.AddForce(direction);
    }
}
