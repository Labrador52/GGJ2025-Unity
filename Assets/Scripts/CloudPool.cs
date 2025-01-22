using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CloudPool : MonoBehaviour
{
    public static CloudPool instance;

    [SerializeField] private GameObject cloud;
    public ObjectPool<GameObject> pool;
    public List<GameObject> activeCloud;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        pool = new ObjectPool<GameObject>(OnCreateCloud, OnGetCloud, OnReleaseCloud, OnDestroyCloud, true, 100, 10000);

        activeCloud = new List<GameObject>();
    }

    private GameObject OnCreateCloud()
    {
        GameObject obj = Instantiate(cloud);
        activeCloud.Add(obj);
        return obj;
    }

    private void OnDestroyCloud(GameObject _obj)
    {
        activeCloud.Remove(_obj);
        Destroy(_obj);
    }

    private void OnGetCloud(GameObject _obj)
    {
        _obj.gameObject.SetActive(true);
        //_obj.GetComponentInChildren<Animator>().SetBool("IsDestory", false);
    }

    private void OnReleaseCloud(GameObject _obj)
    {
        _obj.gameObject.SetActive(false);
    }

    public void ReleaseAllCloud()
    {
        for (int i = 0; i < activeCloud.Count; i++)
        {
            if (!activeCloud[i].activeSelf) continue;

            pool.Release(activeCloud[i]);

        }
        //activeCloud.Clear();

    }
}
