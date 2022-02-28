using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Puzzle.Scoreboard
{
    public class RowScoreboardController : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameTextField;
        [SerializeField] private TMP_Text timeTextField;
        [SerializeField] private TMP_Text positionTextField;

        public void Init(string name, string time, string position)
        {
            nameTextField.text = name;
            timeTextField.text = time;
            positionTextField.text = position;
        }
    }
}