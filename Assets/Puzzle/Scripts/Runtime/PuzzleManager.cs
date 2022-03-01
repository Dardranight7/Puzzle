using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] List<Transform> puzzleParts = new List<Transform>();
    [SerializeField] List<Transform> slots = new List<Transform>();
    [SerializeField] Transform puzzlePartsParent, slotsParent;
    [SerializeField] Transform pivotPoint1, pivotPoint2;
    

    void Start()
    {
        for (int i = 0; i < puzzlePartsParent.childCount; i++)
        {
            Transform childObject = puzzlePartsParent.GetChild(i);
            puzzleParts.Add(childObject);
            Transform childObject2 = slotsParent.GetChild(i);
            slots.Add(childObject2);
            childObject.GetComponent<PuzzlePiece>().OnReleasePiece += OnReleasePiece;
        }
    }

    public void OnReleasePiece(PuzzlePiece puzzlePiece)
    {
        if (puzzlePiece.transform.position.x > pivotPoint1.position.x 
            && puzzlePiece.transform.position.x < pivotPoint2.position.x 
            && puzzlePiece.transform.position.y < pivotPoint1.position.y 
            && puzzlePiece.transform.position.y > pivotPoint2.position.y)
        {
            CheckVictory();
        }
    }

    public void CheckVictory()
    {
        for (int i = 0; i < puzzleParts.Count; i++)
        {
            if (puzzleParts[i].position != slots[i].position)
            {
                return;
            }
        }
        Debug.Log("Ganaste");
    }
}
