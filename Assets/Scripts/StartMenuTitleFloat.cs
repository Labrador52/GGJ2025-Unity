using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuTitleFloat : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 0.5f;
    [SerializeField] private float floatHeight = 0.5f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0);
    }
}
