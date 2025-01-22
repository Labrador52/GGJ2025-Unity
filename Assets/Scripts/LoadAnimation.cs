using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LoadAnimation : MonoBehaviour
{
    private Animator anim;
    public static LoadAnimation instance;

    public void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.gameObject.SetActive(false);
    }

    public void PlayLoadAnimation()
    {
        anim.gameObject.SetActive(true);
        anim.SetTrigger("In");
        anim.SetBool("IsWhite",true);
        StartCoroutine(DoneLoad());
    }

    private IEnumerator DoneLoad()
    {
        yield return new WaitForSeconds(1.3f);
        anim.SetTrigger("Out");
        anim.SetBool("IsWhite", false);
        yield return new WaitForSeconds(0.7f);
        anim.gameObject.SetActive(false);
    }
}
