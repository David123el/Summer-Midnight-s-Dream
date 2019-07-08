using UnityEngine;
using System.Collections.Generic;

public class CollisionDetector : MonoBehaviour
{
    private List<Transform> go = new List<Transform>();
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!go.Contains(collision.gameObject.transform.parent))
        {
            go.Add(collision.gameObject.transform.parent);
            Debug.Log(collision.gameObject.transform.parent);
        }

        if (go.Count == 2)
        {
            EventManager.SwitchBabyTeddy();

            //Debug.Log("switch");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (go.Contains(collision.gameObject.transform.parent))
        {
            go.Remove(collision.gameObject.transform.parent);
            Debug.Log(collision.gameObject.transform.parent);
        }
    }
}
