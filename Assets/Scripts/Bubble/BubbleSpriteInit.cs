using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpriteInit : MonoBehaviour
{
    [SerializeField] private Sprite bubbleSpriteFrontEmpty;
    [SerializeField] private Sprite bubbleSpriteBackEmpty;
    [SerializeField] private Sprite bubbleSpriteFrontFill;
    [SerializeField] private Sprite bubbleSpriteBackFill;

    private void Awake()
    {
        BubbleImageController.SetSprite(bubbleSpriteFrontEmpty, bubbleSpriteBackEmpty, bubbleSpriteFrontFill, bubbleSpriteBackFill);
        Destroy(gameObject);
    }
}
