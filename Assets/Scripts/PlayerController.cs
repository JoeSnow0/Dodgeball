using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public static int score;
    public static int lives;

    public CameraHelper cameraHelper { get { return Camera.main.GetComponent<CameraHelper>(); } }
    public SpriteRenderer spriteRenderer { get { return GetComponent<SpriteRenderer>(); } }
    public float sensitivity;
    public GameObject gameOverText;
    private int m_sizeStep = 1;

    public float speed;

    private Rigidbody2D rb;
    public Rigidbody2D projectileRigidbody;
    [SerializeField] Transform projectileSpawn;
    float moveHorizontal;
    float moveVertical;
    float angle;

    public void Reset()
    {
        score = 0;
        lives = 2;
    }

    void Start()
    {
        Reset();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        //Movement
        moveHorizontal = Input.GetAxis("MovementHorizontal");
        moveVertical = Input.GetAxis("MovementVertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed;

        //Rotation
        var angH = Input.GetAxis("RotationHorizontal");
        var angV = Input.GetAxis("RotationVertical");

        if (angH != 0.0f || angV != 0.0f)
        {
            angle = Mathf.Atan2(angH, angV) * Mathf.Rad2Deg;
        }
        transform.eulerAngles = new Vector3(0, 0, -angle);

        //Death
        gameOverText.SetActive(lives < 0);
        
        //Shooting
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        Rigidbody2D newProjectile =  Instantiate(projectileRigidbody, projectileSpawn.position, gameObject.transform.rotation);
    }
}