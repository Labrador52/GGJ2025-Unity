using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Vector2 _position;
    public Vector2 Position
    {
        get
        {
            return _position;
        }
    }

    [SerializeField] private GameObject bubbleSpriteGameObject;
    [SerializeField] private GameObject bubbleShadowGameObject;

    [SerializeField] private CircleCollider2D TriggerBody;
    [SerializeField] private CircleCollider2D TriggerPickup;
    [SerializeField] private CircleCollider2D TriggerFog;

    private static float triggerRadiusBody;
    private static float triggerRadiusPickup;
    private static float triggerRadiusFog;

    private float bubbleFloat;
    private static float solidHeight;

    private void Awake()
    {
        if (bubbleSpriteGameObject == null)
        {
            Debug.LogError("Bubble Sprite GameObject is not assigned in the inspector");
        }
        if (bubbleShadowGameObject == null)
        {
            Debug.LogError("Bubble Shadow GameObject is not assigned in the inspector");
        }

        TriggerBody.radius = triggerRadiusBody;
        TriggerPickup.radius = triggerRadiusPickup;
        TriggerFog.radius = triggerRadiusFog;

        _position = new Vector2(0.0f, 0.0f);
        solidHeight = 1.0f; // The height of the Buggle, TEMPORARY
    }

    private void Update()
    {
#region Floating Animation
        bubbleFloat = Mathf.Sin(Time.time * 2) * 0.1f;
        bubbleFloat += solidHeight;
        bubbleSpriteGameObject.transform.localPosition = new Vector2(0.0f, bubbleFloat);
#endregion

    }


    /// <summary>
    /// Spawn Buggle on the given position
    /// </summary>
    /// <param name="Spawn Tile Cell position"></param>
    public void Spawn(Vector2 position)
    {
        _position = position;
        // Set the position of the Buggle
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="triggerType"></param>
    /// <param name="range"></param>
    public static void SetTriggerRange(TriggerType triggerType, float range)
    {
        // Debug.Log("Set Trigger Range, TriggerType:" + triggerType + ", Radiu:" + range);
        switch (triggerType)
        {
            case TriggerType.Body:
                triggerRadiusBody = range;
                break;
            case TriggerType.Pickup:
                triggerRadiusPickup = range;
                break;
            case TriggerType.Fog:
                triggerRadiusFog = range;
                break;
            default:
                break;
        }
    }

    public static void SetSolidHeight(float height)
    {
        solidHeight = height;
    }

}

public enum TriggerType
{
    Body,
    Pickup,
    Fog
}