using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public CameraHelper cameraHelper { get { return Camera.main.GetComponent<CameraHelper> ( ); } }
    public SpriteRenderer spriteRenderer { get { return GetComponent<SpriteRenderer> ( ); } }
    public ScreenShake screenShake { get { return Camera.main.GetComponent<ScreenShake> ( ); } }
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
    private float m_MoveSpeed;

    // Use this for initialization
    void Start () {
        m_AudioSource = GetComponent<AudioSource>();
        //SetPaddle ( true );
	}
	
	// Update is called once per frame
	void Update () {
        //if ( m_OnPaddle && Input.GetButtonDown ( "Fire1"))
            //SetPaddle ( false );

        //if ( !m_OnPaddle ) {
        transform.position += (Vector3)m_Velocity * Time.deltaTime;

            //Vector3 collisionNormal;
            //if(cameraHelper.IsOutside(renderer.bounds, out collisionNormal ) ) {
            //    float collDot = Vector3.Dot ( collisionNormal.normalized, Vector3.up );
            //    if(Mathf.Approximately(collDot, 1 ) ) {
            //        AudioHelper.PlayOneShot(m_AudioSource, dieSfx);

            //        SetPaddle ( true );
            //        screenShake.ShakeIt ( 0.5f );
            //        Paddle.lives--;

            //        if (Paddle.lives < 0)
            //            gameObject.SetActive(false);
            //    } else {
            //        AudioHelper.PlayOneShot(m_AudioSource, bounceSfx, 0.5f, 1f);

            //        transform.position += collisionNormal;
            //        Bounce ( collisionNormal.normalized );
            //        //screenShake.ShakeIt ( 0.1f );
            //    }
            //}
             	
	}

    void OnCollisionEnter2D(Collision2D coll) {
        //AudioHelper.PlayOneShot(m_AudioSource, bounceSfx, 0.5f, 0.5f);
        if(coll.gameObject.CompareTag("Player"))
        {
            CollisionWithPlayer();
        }
        else
        {
            Bounce(coll.contacts[0].normal);
        }
    }

    private void CollisionWithPlayer()
    {
        Destroy(gameObject);
    }

    void Bounce(Vector2 normal) {
        Vector2 dir = m_Velocity.normalized;

        if ( Vector2.Dot ( dir, normal ) > 0 )
            return;

        SetDirection ( Vector2.Reflect ( dir, normal ) );
    }

    void SetDirection(Vector2 direction) {
        m_Velocity = direction * m_MoveSpeed;
    }

    public void SetPaddle(bool onPaddle) {
        m_OnPaddle = onPaddle;
        transform.SetParent ( onPaddle ? paddle.transform : null );

        if ( !onPaddle ) {
            m_MoveSpeed = moveSpeed;
            AudioHelper.PlayOneShot(m_AudioSource, shootSfx);

            Vector2 dir = new Vector2 ( 0.707f, 0.707f );
            if ( Random.Range ( 0, 2 ) == 1 )
                dir.x = -dir.x;

            SetDirection ( dir );
        } else {
            transform.localPosition = paddleOffset;
        }
    }
}
