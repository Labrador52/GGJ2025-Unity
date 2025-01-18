using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Vector2 _position;
    [SerializeField] private Vector2 _destination;
    [SerializeField] private int _energy;
    public Vector2 Position
    {
        get
        {
            return _position;
        }
    }

    [SerializeField] public bool isFacingRight;
    [SerializeField] public bool isFacingFront;

    [SerializeField] public bool isFill;

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

    private float AnimationDeltaTime;

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

        AnimationDeltaTime = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
    }

    private void Update()
    {
#region Floating Animation
        bubbleFloat = Mathf.Sin(Time.time * 2 + AnimationDeltaTime) * 0.1f;
        bubbleFloat += solidHeight;
        bubbleSpriteGameObject.transform.localPosition = new Vector2(0.0f, bubbleFloat);
#endregion

    }

    private void FixedUpdate()
    {
#region Old Movement Method
        // // Calculate the distance between the current position and the destination
        // float distance = Vector2.SqrMagnitude(_position - _destination);

        // if (distance > 0.025f)
        // {
        //     // Calculate speed with energy
        //     float speed = (_energy >= 4)? 0.1f : 0.05f;
        //     //move to the destination
        //     _position = Vector2.MoveTowards(_position, _destination, speed);
        //     // Apply the new position to the Buggle
        //     transform.position = _position;
        // }
        // else
        // {
        //     _energy -= 1;
        //     if (_energy <= 0)
        //     {
        //         _energy = 0;
        //         // play the death animation
        //     }

        //     _destination += new Vector2(1.0f, 0.5f);
        // }
#endregion

    
    }

    public void SetDestination(Vector2 destination)
    {
        _destination = destination;
    }

    /// <summary>
    /// Spawn Buggle on the given position
    /// </summary>
    /// <param name="Spawn Tile Cell position"></param>
    public static void Spawn(Vector2 position, Vector2 destination)
    {
        // 实例化Bubble预制体
        GameObject bubble = Instantiate(PrefabManager.Instance.BubblePrefab, position, Quaternion.identity) as GameObject;
        bubble.GetComponent<Bubble>().SetDestination(destination);
        // Set Father
        bubble.transform.SetParent(Gameplay.Instance.gameObject.transform);
        // _position = position;
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