using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public Collider2D ReturnCollider2D(Vector3 mousePos)
    {
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, worldPos);

        if (hit.collider != null)
        {
            return hit.collider;
        }

        return null;
    }
}
