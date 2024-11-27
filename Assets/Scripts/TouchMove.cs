using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchMove : MonoBehaviour, IBeginDragHandler , IDragHandler
{
    [SerializeField]
    private Player player;


    public float speed;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
   
}
