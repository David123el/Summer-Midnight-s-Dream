using UnityEngine;

public class CanvasCameraAttacher : MonoBehaviour
{
    private void OnEnable()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.worldCamera.transform.position = new Vector3(canvas.worldCamera.transform.position.x, canvas.worldCamera.transform.position.y, -10);
    }
}
