using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFogTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log("Triggered");
        if (collision.gameObject.tag == "Fog")
        {
            //Destroy(collision.gameObject);
            FogPool.instance.pool.Release(collision.gameObject);
        }
    }
}
