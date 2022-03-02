using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundPieces : MonoBehaviour, IPointerUpHandler
{
    public Action<PuzzlePiece> OnReleasePiece;
    public Action<bool> OnTakePiece;
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
