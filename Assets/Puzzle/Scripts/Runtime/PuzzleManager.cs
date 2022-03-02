using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] List<Transform> puzzleParts = new List<Transform>();
    [SerializeField] List<Transform> slots = new List<Transform>();
    [SerializeField] Transform puzzlePartsParent, slotsParent;
    [SerializeField] Transform pivotPoint1, pivotPoint2, pivotPoint3, pivotPoint4;

    bool firstMovement = true;
    bool isGameRuning;
    float startTime;
    float endTime;

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
        puzzlePartsParent.gameObject.SetActive(false);
        slotsParent.gameObject.SetActive(false);
        MixPieces();
        puzzlePartsParent.gameObject.SetActive(true);
        slotsParent.gameObject.SetActive(true);
    }

    public void OnReleasePiece(PuzzlePiece puzzlePiece)
    {
        if (firstMovement)
        {
            StartGame();
            firstMovement = false;
        }
        if (puzzlePiece.transform.position.x > pivotPoint1.position.x
            && puzzlePiece.transform.position.x < pivotPoint2.position.x
            && puzzlePiece.transform.position.y < pivotPoint1.position.y
            && puzzlePiece.transform.position.y > pivotPoint2.position.y)
        {
            CheckVictory();
        }
    }

    public void MixPieces()
    {
        for (int i = 0; i < puzzleParts.Count; i++)
        {
            puzzleParts[i].transform.position = new Vector2(
                Random.Range(pivotPoint3.position.x, pivotPoint4.position.x),
                Random.Range(pivotPoint3.position.y, pivotPoint4.position.y)
                );
        }
    }

    public void StartGame()
    {
        startTime = Time.time;
        isGameRuning = true;
    }

    private void Update()
    {
        if (isGameRuning)
        {
            uIManager.SetTimeText((Time.time - startTime).ToString("00:00"));
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
        endTime = Time.time;
        isGameRuning = false;
        Debug.Log("Ganaste");
    }
}
