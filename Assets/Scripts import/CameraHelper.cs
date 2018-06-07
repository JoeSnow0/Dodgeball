using UnityEngine;
using System.Collections;

public class CameraHelper : MonoBehaviour {
    private Bounds m_CameraBounds;

	void Start () {
        Vector3 cameraCenter = transform.position;
        cameraCenter.z = 0;

        Vector3 viewportSize = new Vector3 ( );
        viewportSize.y = GetComponent<Camera> ( ).orthographicSize;
        viewportSize.x = viewportSize.y * ( ( float ) Screen.width / ( float ) Screen.height );
        viewportSize.z = 10;

        m_CameraBounds = new Bounds ( cameraCenter, viewportSize * 2 );
	}

    public bool IsOutside(Bounds bounds, out Vector3 depenetration) {
        Bounds cameraBounds = m_CameraBounds;
        cameraBounds.size -= bounds.size;

        if ( cameraBounds.Contains ( bounds.center ) ) {
            depenetration = Vector3.zero;
            return false;
        }

        depenetration = cameraBounds.ClosestPoint ( bounds.center ) - bounds.center;
        return true;
    }
}
