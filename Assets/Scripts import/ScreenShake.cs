using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
    public float magnitude;
    public float interval;
    public AnimationCurve curve;

    private Vector3 m_StartPos;
    
	void Start () {
        m_StartPos = transform.position;
	}

    public void ShakeIt(float duration) {
        StopAllCoroutines ( );
        StartCoroutine ( ShakeRoutine ( duration ) );
    }

    IEnumerator ShakeRoutine(float duration) {
        float startTime = Time.time;

        while(Time.time - startTime < duration ) {
            float mag = 1 - ( ( Time.time - startTime ) / duration );
            Vector3 shakeVector = Vector3.Normalize ( new Vector3 ( Random.Range ( -1f, 1f ), Random.Range ( -1f, 1f ), 0 ) );
            Vector3 shakePos = m_StartPos + shakeVector * curve.Evaluate ( mag ) * magnitude;

            float shakeTime = Time.time;
            while(Time.time - shakeTime < interval ) {
                transform.position = Vector3.Lerp ( shakePos, m_StartPos, ( Time.time - shakeTime ) / interval );
                yield return null;
            }

            yield return null;
        }

        transform.position = m_StartPos;
    }
}
