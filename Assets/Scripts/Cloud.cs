using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform child;
    private bool isPlayAnimation;
    //private void Start()
    //{
    //    anim = GetComponent<Animator>();
    //}

    private void OnEnable()
    {
        sr.color = Color.white;
        sr.sortingOrder = 5;
        isPlayAnimation = false;
    }

    public void DestroySelfAnimation(int _index)
    {
        if (isPlayAnimation)
            return;

        isPlayAnimation = true;
        if (_index == 1)
            anim.SetTrigger("Left");
        else
            anim.SetTrigger("Right");
    }

}