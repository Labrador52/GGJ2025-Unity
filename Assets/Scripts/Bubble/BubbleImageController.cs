using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleImageController : MonoBehaviour
{
    private static Sprite spriteBubbleFrontEmpty;
    private static Sprite spriteBubbleBackEmpty;
    private static Sprite spriteBubbleFrontFill;
    private static Sprite spriteBubbleBackFill;

    [SerializeField] private Bubble bubble;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (bubble.isFill)
        {
            spriteRenderer.sprite = bubble.isFacingFront? spriteBubbleFrontFill: spriteBubbleBackFill;
        }
        else
        {
            spriteRenderer.sprite = bubble.isFacingFront? spriteBubbleFrontEmpty: spriteBubbleBackEmpty;
        }
        
        spriteRenderer.flipX = !bubble.isFacingRight;
    }


    public static void SetSprite(Sprite spriteFront, Sprite spriteBack, Sprite spriteFrontFill, Sprite spriteBackFill)
    {
        spriteBubbleFrontEmpty = spriteFront;
        spriteBubbleBackEmpty = spriteBack;
        spriteBubbleFrontFill = spriteFrontFill;
        spriteBubbleBackFill = spriteBackFill;
    }
}
