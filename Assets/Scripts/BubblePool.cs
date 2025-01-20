using UnityEngine;
using UnityEngine.Pool;

public class BubblePool : MonoBehaviour
{
    public static BubblePool instance;

    [SerializeField] private GameObject bubble;
    public ObjectPool<GameObject> pool;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        pool = new ObjectPool<GameObject>(() => Instantiate(bubble),
            (GameObject obj) => obj.gameObject.SetActive(true),
            (GameObject obj) => obj.gameObject.SetActive(false),
           (GameObject obj) => Destroy(obj), true, 1000, 10000);
    }
}
