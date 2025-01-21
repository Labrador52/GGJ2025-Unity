using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FogPool : MonoBehaviour
{
    public static FogPool instance;

    [SerializeField] private GameObject fog;
    public ObjectPool<GameObject> pool;
    public List<GameObject> activeFog;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        pool = new ObjectPool<GameObject>(OnCreateFog, OnGetFog, OnReleaseFog, OnDestroyFog, true, 2500, 10000);

        activeFog = new List<GameObject>();
    }

    private GameObject OnCreateFog()
    {
        GameObject obj = Instantiate(fog);
        activeFog.Add(obj);
        return obj;
    }

    private void OnDestroyFog(GameObject _obj)
    {
        activeFog.Remove(_obj);
        Destroy(_obj);
    }

    private void OnGetFog(GameObject _obj)
    {
        _obj.gameObject.SetActive(true);
    }

    private void OnReleaseFog(GameObject _obj)
    {
        _obj.gameObject.SetActive(false);
    }

    public void ReleaseAllFog()
    {
        for (int i = 0; i < activeFog.Count; i++)
        {
            if (!activeFog[i].activeSelf) continue;

            pool.Release(activeFog[i]);

        }
        activeFog.Clear();

    }
}
