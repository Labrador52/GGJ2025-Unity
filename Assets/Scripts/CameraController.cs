using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private static CameraController _instance;
    public static CameraController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Camera Controller").GetComponent<CameraController>();  // 后续在一个初始化方法中初始化所有单例
            }
            return _instance;
        }
    }
    [SerializeField] private GameObject cameraGameObject;
    private Camera mainCamera;
    [SerializeField] private float cameraMoveSpeed = 5.0f;    // Camera move speed, unit: m/s

    private void Awake()
    {
        if (cameraGameObject == null)
        {
            Debug.LogError("Camera GameObject is not assigned in the inspector");
            return;
        }
        mainCamera = cameraGameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Camera component is not found in the camera GameObject");
            return;
        }

        if (Gameplay.Instance.isPlaying == false)
        {
            return;
        }

        // Move the camera

        if (Input.GetKey(KeyCode.W))
        {
            mainCamera.transform.position += mainCamera.transform.up * cameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.position -= mainCamera.transform.up * cameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.position -= mainCamera.transform.right * cameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.position += mainCamera.transform.right * cameraMoveSpeed * Time.deltaTime;
        }

        // set Camera Size

        float scrollDelta = Input.GetAxis("Mouse ScrollWheel"); // 获取滚轮滚动值

        if (scrollDelta != 0)
        {
            mainCamera.orthographicSize -= scrollDelta * 2.0f; // 改变正交尺寸

            // 限制缩放范围
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 3.0f, 10.0f);
        }
    }

    public void SetSpeed(float speed)
    {
        // Set the camera move speed
        cameraMoveSpeed = speed;

    }

    public void ResetPosition()
    {
        gameObject.transform.position = new Vector3(0, 0, -10);
    }
    public void ResetPosition(Vector3 position)
    {
        gameObject.transform.position = position + new Vector3(0, 2.5f, -10);
    }
}
