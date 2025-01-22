using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Vector2 _position;
    [SerializeField] private Vector2 _destination;
    [SerializeField] private int energy;
    [SerializeField] private float velocity;
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

    [SerializeField] public int lifeLeft;

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
    
        velocity = 0.05f;
        lifeLeft = 120000;
        energy = 8;
    }

    #region 修改1
    private void OnEnable()
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

        velocity = 0.05f;
        lifeLeft = 120000;
        energy = 8;

        
        isFacingRight = true;
        isFacingFront = false;
        isFill = false;
        _destination = Vector2.zero;

    }
    #endregion

    private void Update()
    {
#region Floating Animation
        bubbleFloat = Mathf.Sin(Time.time * 2 + AnimationDeltaTime) * 0.1f;
        bubbleFloat += solidHeight;
        bubbleSpriteGameObject.transform.localPosition = new Vector2(0.0f, bubbleFloat);
        #endregion

        if (!Gameplay.Instance.FogMode)
            return;

        MapManager.instance.ClearSurroundingFog(transform.position, isFacingRight, isFacingFront);
    }

    private void FixedUpdate()
    {
#region Old Movement Method
        // // Calculate the distance between the current position and the destination

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

        // life
        lifeLeft -= 1;
        if (lifeLeft <= 0)
        {
            Die();
            return;
        }

        // move
        transform.position = Vector2.MoveTowards(transform.position, _destination, velocity);

        float distance = Vector2.SqrMagnitude((Vector2)transform.position - _destination);

        if (distance <= 0.0001f)
        {
            transform.position = _destination;
            ArriveDestination();
        }

    }

    /// <summary>
    /// Get Tilemap cell position
    /// </summary>
    /// <returns></returns>
    private Vector3 GetCellPosition()
    {
        return BuildingManager.instance.constructionLayer.tilemap.WorldToCell(transform.position);
    }

    public void SetDestination(Vector2 destination)
    {
        _destination = destination;
    }

    private void ArriveDestination()
    {
        // Debug.Log("Arrive!");
        energy -= 1;
        energy = math.max(energy, 0);

#region Check Fan Effect
        Vector3Int fanEffect = MapManager.instance.CheckFanEffectRange(transform.position);
        // check

        //Debug.Log("Fan Effect: " + fanEffect);

        if (fanEffect != Vector3.zero)
        {
            // string facing;
            if (fanEffect.x == 7)
            {
                // facing = "BR";
                isFacingRight = true;
                isFacingFront = false;
            }
            else if (fanEffect.x == -7)
            {
                // facing = "FL";
                isFacingRight = false;
                isFacingFront = true;
            }
            else if (fanEffect.y == 7)
            {
                // facing = "FR";
                isFacingRight = false;
                isFacingFront = false;
            }
            else
            {
                // facing = "BL";
                isFacingRight = true;
                isFacingFront = true;
            }
            
            energy = 16;
        }
#endregion

#region Check Pickup
        if (MapManager.instance.IsGetMail(transform.position))
        {
        PickUp();
        }

#endregion

#region Check Arrive
        if (isFill)
        {
            if (MapManager.instance.IsArrive(transform.position))
            {
                // Debug.Log("Arrive!");
                Gameplay.Instance.Win();
                // Destroy(gameObject);
                // return;
            }
        }

#endregion

        _destination += new Vector2(isFacingRight? 0.5f: -0.5f, isFacingFront? -0.25f: 0.25f);

        velocity = (energy >= 4)? 0.05f: 0.025f;
    }

    private void PickUp()
    {
        // fill
        if (isFill)
        {
            return;
        }
        isFill = true;
    }

    internal void Die()
    {
        //Destroy(gameObject);
        BubblePool.instance.pool.Release(gameObject);
    }

    public static void Spawn()
    {
        Spawn(Vector2.zero, new Vector2(0.5f, 0.25f));
    }

    public static void Spawn(Vector2 position)
    {
        Spawn(position, new Vector2(0.5f, 0.25f));
    }

    /// <summary>
    /// Spawn Buggle on the given position
    /// </summary>
    /// <param name="Spawn Tile Cell position"></param>
    public static void Spawn(Vector2 position, Vector2 destination)
    {
        position += new Vector2(0.0f, 0.25f);
        // 实例化Bubble预制体
        //GameObject bubble = Instantiate(PrefabManager.Instance.BubblePrefab, position, Quaternion.identity) as GameObject;
        GameObject bubble = BubblePool.instance.pool.Get();
        bubble.transform.position = position;
        // set name as Bubble
        bubble.name = "Bubble";
        // set parent as Bubbles
        bubble.transform.SetParent(GameObject.Find("Bubbles").transform);
        //set Tag as Bubble
        // bubble.tag = "Bubble";
        bubble.GetComponent<Bubble>().SetDestination(destination);
        // Set Father
        bubble.transform.SetParent(Gameplay.Instance.gameObject.transform);
        // _position = position;
        // Set the position of the Buggle
        bubble.GetComponent<Bubble>()._destination = position + new Vector2(0.5f, 0.25f);
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