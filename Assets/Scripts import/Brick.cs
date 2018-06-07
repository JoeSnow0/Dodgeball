using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D coll) {
        //gameObject.SetActive ( false );
        Paddle.score += 100;
        Destroy(gameObject, 0.1f);
    }
}
