using UnityEngine;

public class shotgunAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void shootanimation()
    {
        animator.SetTrigger("shoot");
    }
}
