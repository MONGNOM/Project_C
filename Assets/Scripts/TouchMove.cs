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
        Debug.Log("������");
        // ó�� ���� ��ǥ �������� �� �Ʒ� �� �� ����
        Debug.Log(Input.mousePosition);
    }
   
}
