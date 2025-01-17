using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject cameraGameObject;
    private Camera mainCamera;

    private void Awake()
    {
        if (cameraGameObject == null)
        {
            Debug.LogError("Camera GameObject is not assigned in the inspector");
            return;
        }
        mainCamera = cameraGameObject.GetComponent<Camera>();
    }
}
