using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnimation : MonoBehaviour
{
    public void DestroySelf()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -1;
        StartCoroutine(ReleaseWithDelay());
    }

    private IEnumerator ReleaseWithDelay()
    {
        yield return new WaitForSeconds(2f);
        CloudPool.instance.pool.Release(transform.parent.gameObject);
    }
}
