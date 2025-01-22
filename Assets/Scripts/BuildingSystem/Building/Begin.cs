using UnityEngine;

public class Begin : MonoBehaviour
{
    private float timer = 0.65f;
    private float time = 2;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!Gameplay.Instance.isPlaying)
            return;

        timer += Time.deltaTime;

        if (timer >= time)
        {
            //Debug.Log("test");
            animator.SetTrigger("trigger");
            timer = 0;
        }
    }

    
}
