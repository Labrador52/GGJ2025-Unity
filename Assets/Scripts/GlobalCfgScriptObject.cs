using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "GlobalCfgScriptObject", order = 1), System.Serializable]
public class GlobalCfgScriptObject : ScriptableObject
{
    [Header("Buggle Aninmation")]
    public float solidHeight = 1.0f;

    [Header("Trigger Range")]
    public float TriggerBody = 0.5f;
    public float TriggerPickup = 1.5f;
    public float TriggerFog = 2.5f;

    [Header("Gameplay Config")]
    public int bubbleSpawnInterval = 100;
    [Range(0.001f, 100.0f)]
    public float cameraMoveSpeed = 5.0f;    // Camera move speed, unit: m/s

}
