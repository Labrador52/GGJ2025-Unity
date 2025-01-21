using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarManager : MonoBehaviour
{
    private static FogOfWarManager _instance;
    public static FogOfWarManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private GameObject _fogOfWarPlane;
    [SerializeField] private GameObject _fogOfWarPlanePrefab;

    [SerializeField] private float _fogOfWarSizeHorizontal;
    [SerializeField] private float _fogOfWarSizeVertical;

    [SerializeField] private float _fogOfWarResolution;

    [SerializeField] private float _fogOfWarRandomPosition;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (_fogOfWarPlane == null)
        {
            Debug.LogError("Fog of War Plane is not assigned in the inspector");
        }
        if (_fogOfWarPlanePrefab == null)
        {
            Debug.LogError("Fog of War Plane Prefab is not assigned in the inspector");
        }
    }

    private void Reset()
    {
        _fogOfWarSizeHorizontal = 50f;
        _fogOfWarSizeVertical = 50f;
        _fogOfWarResolution = 1.0f;
    }


    [ContextMenu("Create Fog of War")]
    public void CreateFog()
    {
        ClearFog();

        if (_fogOfWarResolution <= 0.0f)
        {
            Debug.LogError("Fog of War Resolution must be greater than 0");
            return;
        }
        for (float i = 0; i < _fogOfWarSizeHorizontal; i += (1.0f / _fogOfWarResolution))
        {
            for (float j = 0; j < _fogOfWarSizeVertical; j += (1.0f / _fogOfWarResolution))
            {
                //GameObject go = Instantiate(_fogOfWarPlanePrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                GameObject go = FogPool.instance.pool.Get();
                go.transform.SetParent(_fogOfWarPlane.transform);
                go.transform.localPosition = new Vector3(i, j, -5.0f);
                // according the Size Of Fog of War Plane, move fog position
                go.transform.localPosition += new Vector3(-0.5f * _fogOfWarSizeHorizontal, -0.5f * _fogOfWarSizeVertical, 0.0f);
                // Randomize the position of the fog
                go.transform.localPosition += new Vector3(Random.Range(-_fogOfWarRandomPosition, _fogOfWarRandomPosition), Random.Range(-_fogOfWarRandomPosition, _fogOfWarRandomPosition), 0.0f);

                // Set children Tag As Fog
                go.tag = "Fog";
            }
        }
        Debug.Log("Creating Fog of War");
    }

    private IEnumerator CreateFogForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CreateFog();
    }

    public void CreateFogWithDelay(float _seconds) => StartCoroutine("CreateFogForSeconds", _seconds); 

    [ContextMenu("Clear Fog of War")]
    public void ClearFog()
    {
        Debug.Log("Clearing Fog of War");
        //while (_fogOfWarPlane.transform.childCount > 0)
        //{
        //    DestroyImmediate(_fogOfWarPlane.transform.GetChild(0).gameObject);
        //}

        FogPool.instance.ReleaseAllFog();
    }
}
