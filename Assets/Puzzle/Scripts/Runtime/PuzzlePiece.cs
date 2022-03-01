using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isDragging = false;
    Vector2 offset = new Vector2();
    public void OnPointerDown(PointerEventData eventData)
    {
        offset.x = transform.position.x - eventData.position.x;
        offset.y = transform.position.y - eventData.position.y;
        transform.SetAsLastSibling();
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            if (Input.touchCount > 0)
            {
                transform.position = Input.touches[0].position + offset;
            }
            else
            {
                transform.position = Input.mousePosition + new Vector3(offset.x,offset.y);
            }
        }
    }
}
