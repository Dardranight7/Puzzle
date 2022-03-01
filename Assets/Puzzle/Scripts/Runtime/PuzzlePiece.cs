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

    private void Start()
    {
        selfImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset.x = transform.position.x - eventData.position.x;
        offset.y = transform.position.y - eventData.position.y;
        transform.SetAsLastSibling();
        selfImage.raycastTarget = false;
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
            OnReleasePiece.Invoke(this);
        }

        isDragging = false;
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
}
