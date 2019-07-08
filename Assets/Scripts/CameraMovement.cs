using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public void MoveCameraUp(float step)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
    }
}
