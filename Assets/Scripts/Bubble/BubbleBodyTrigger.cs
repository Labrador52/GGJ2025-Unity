using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBodyTrigger : MonoBehaviour
{
    [SerializeField] private Bubble bubble;
    [SerializeField] private CircleCollider2D circleCollider2D;

    // check trigger Stay
    private void OnTriggerStay2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Mountain" || tag == "Contaminate")
        {
            bubble.Die();
        }
    }
}
