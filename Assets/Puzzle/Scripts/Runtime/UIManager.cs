using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText; 

    public void SetTimeText(string data)
    {
        timeText.text = data;
    }
}
