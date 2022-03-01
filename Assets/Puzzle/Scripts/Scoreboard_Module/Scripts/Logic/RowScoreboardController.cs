using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.Scoreboard
{
    public class RowScoreboardController : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameTextField;
        [SerializeField] private TMP_Text timeTextField;
        [SerializeField] private TMP_Text positionTextField;
        [SerializeField] private Image frame;
        [SerializeField] private Color firstPositionFrameColor;

        public void Init(string name, string time, string position, bool firstPosition = false)
        {
            nameTextField.text = name;
            timeTextField.text = time;
            positionTextField.text = position;
            if (firstPosition)
            {
                frame.color = firstPositionFrameColor;
                nameTextField.color = Color.white;
                timeTextField.color = Color.white;
                positionTextField.color = Color.white;
            }
        }
    }
}