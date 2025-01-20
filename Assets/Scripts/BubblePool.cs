using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Pool;

public class BubblePool : MonoBehaviour
{
    public static BubblePool instance;

    [SerializeField] private GameObject bubble;
    [SerializeField] private Transform bubbleParent;
    public ObjectPool<GameObject> pool;
    public List<GameObject> activeBubbles;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        pool = new ObjectPool<GameObject>(OnCreateBubble, OnGetBubble, OnReleaseBubble, OnDestroyBubble, true, 100, 10000);

        activeBubbles = new List<GameObject>();
    }

    private GameObject OnCreateBubble()
    {
        GameObject obj = Instantiate(bubble);
        activeBubbles.Add(obj);
        return obj;
    }

    private void OnDestroyBubble(GameObject _obj)
    {
        activeBubbles.Remove(_obj);
        Destroy(_obj);
    }

    private void OnGetBubble(GameObject _obj)
    {
        //activeBubbles.Add(_obj);
        _obj.gameObject.SetActive(true);
    }

    private void OnReleaseBubble(GameObject _obj)
    {
        //activeBubbles.Remove(_obj);
        _obj.gameObject.SetActive(false);
    }

    public void ReleaseAllBubble()
    {
        for(int i = 0; i < activeBubbles.Count; i++)
        {
            if (!activeBubbles[i].activeSelf) continue;

            pool.Release(activeBubbles[i]);

        }
        activeBubbles.Clear();
        //Debug.Log("É¾³ýÍê³É");
    }
}
