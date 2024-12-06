using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Description : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void DescriptionActive()
    {
        animator.SetTrigger("Show");
    }

    public void UnDescriptionActive()
    {
        animator.SetTrigger("Hide");
    }

}
