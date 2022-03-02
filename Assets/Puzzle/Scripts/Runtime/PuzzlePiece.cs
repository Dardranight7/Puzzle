using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Image selfImage;
    bool isDragging = false;
    Vector2 offset = new Vector2();
    public Action<PuzzlePiece> OnReleasePiece;
    public Action<bool> OnTakePiece;

    private void Start()
    {
        selfImage = GetComponent<Image>();
    }

    private void LateUpdate()
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void SetRaycast(bool state)
    {
        selfImage.raycastTarget = state;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset.x = transform.position.x - eventData.position.x;
        offset.y = transform.position.y - eventData.position.y;
        transform.SetAsLastSibling();
        OnTakePiece.Invoke(false);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            GameObject onReleaseObject = eventData.pointerCurrentRaycast.gameObject;
            if (onReleaseObject.CompareTag("Slot"))
            {
                transform.position = onReleaseObject.transform.position;
            }
            selfImage.raycastTarget = true;
            OnTakePiece.Invoke(true);
            OnReleasePiece.Invoke(this);
        }

        isDragging = false;
    }
}
