using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchMove : MonoBehaviour, IBeginDragHandler , IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("움직임");
        // 처음 시작 좌표 가져오고 위 아래 양 옆 측정
        Debug.Log(Input.mousePosition);
    }
   
}
