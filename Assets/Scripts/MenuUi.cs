using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUi : MonoBehaviour
{
    Animator animator;
    public Button button;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void MenuUiShow()
    {
        animator.SetTrigger("Show");
        button.transform.localScale = Vector3.zero;
    }

    public void MenuUiHide()
    {
        animator.SetTrigger("Hide");
    }

    public void ShowButton()
    {
        button.transform.localScale = Vector3.one;
    }
}
