using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public CameraHelper cameraHelper { get { return Camera.main.GetComponent<CameraHelper>(); } }
    public SpriteRenderer spriteRenderer { get { return GetComponent<SpriteRenderer>(); } }
    public ScreenShake screenShake { get { return Camera.main.GetComponent<ScreenShake>(); } }
    int owner;
    public GameObject paddle;
    public Vector3 paddleOffset;
    public float moveSpeed;
    public float speedIncrease;

    public AudioClip bounceSfx;
    public AudioClip dieSfx;
    public AudioClip shootSfx;

    private bool m_OnPaddle = true;
    private Vector2 m_Velocity;
    private AudioSource m_AudioSource;
    //private float m_MoveSpeed;

    // Use this for initialization
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        //SetPaddle ( true );
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)m_Velocity * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //AudioHelper.PlayOneShot(m_AudioSource, bounceSfx, 0.5f, 0.5f);
        if (coll.gameObject.CompareTag("Player"))
        {
            CollisionWithPlayer();
        }
        else
        {
            //Outdated?
            Bounce(coll.contacts[0].normal);
        }
    }

    private void CollisionWithPlayer()
    {
        Destroy(gameObject);
    }

    void Bounce(Vector2 normal)
    {
        Vector2 dir = m_Velocity.normalized;

        if (Vector2.Dot(dir, normal) > 0)
            return;

        SetDirection(Vector2.Reflect(dir, normal));
    }

    public void SetDirection(Vector2 direction)
    {
        m_Velocity = direction * moveSpeed;
    }
}
