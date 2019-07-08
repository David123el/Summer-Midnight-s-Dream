using UnityEngine;

public class PuckDetector : MonoBehaviour
{
    private Collider2D puckCol;

    public Collider2D ReturnPuckCol()
    {
        if (puckCol != null)
        {
            return puckCol;
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Puck")
            {
                puckCol = collision;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Puck")
            {
                puckCol = null;
            }
        }
    }
}
