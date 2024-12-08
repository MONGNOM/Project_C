using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Description : MonoBehaviour
{
    Animator animator;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void DescriptionActive(Slot slotItem)
    {
       itemName.text = slotItem.name;
       itemDescription.text = slotItem.description;
       animator.SetTrigger("Show");
    }

    public void UnDescriptionActive()
    {
        animator.SetTrigger("Hide");
    }

}
