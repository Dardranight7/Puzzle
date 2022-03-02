using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puzzle.Scoreboard
{
    public class ScoreboardController : MonoBehaviour
    {
        [SerializeField] private Transform scoreboardTable;
        [SerializeField] private RowScoreboardController scoreRowPrefab;
        [SerializeField] JsHandler jsHandler;
        public void OnEnable()
        {
            foreach (Transform child in scoreboardTable)
                Destroy(child.gameObject);

            InitializeTable();
        }

        private void InitializeTable()
        {
            jsHandler.TomarDatos();
        }

        public void ReceiveUserDataFromOut(string data)
        {
            RequestUsersCallback(MyOwnParser(data));
            
        }

        public List<UserDto> MyOwnParser(string data)
        {
            List<UserDto> UserDtos = new List<UserDto>();
            List<string> Objects = new List<string>();
            int keysCounter = -1;
            string currentText = "";
            foreach (var character in data)
            {
                if (keysCounter == -1)
                {
                    keysCounter++;
                    continue;
                }
                if (character == '{' || character == '}')
                {
                    keysCounter++;
                }
                if (keysCounter > 0)
                {
                    currentText += character;
                }
                if (keysCounter > 1)
                {
                    Objects.Add(currentText);
                    keysCounter = -1;
                    currentText = "";
                }
            }
            foreach (var Object in Objects)
            {
                UserDtos.Add(JsonConvert.DeserializeObject<UserDto>(Object));
            }
            return UserDtos;
        }

        public void RequestUsersCallback(List<UserDto> data)
        {
            int counter = 1;
            data.Sort(CompareSort);

            foreach (var user in data)
            {
                RowScoreboardController row = Instantiate(scoreRowPrefab, scoreboardTable);
                row.Init($"{user.nombre} {user.apellido}", GiveTimeFormatter((int)user.scoreSeg), counter.ToString(), counter == 1);
                counter++;
            }
        }

        public string GiveTimeFormatter(int scoreSeg)
        {
            string finalString = "";
            if (scoreSeg < 60)
            {
                if (scoreSeg < 10)
                    finalString = $"0:00:0{scoreSeg}";
                else
                    finalString = $"0:00:{scoreSeg}";
            }
            else if (scoreSeg < 3600)
            {
                int scoreMin = scoreSeg / 60;
                int restSeg = scoreSeg % 60;

                if (scoreMin < 10)
                    finalString += $"0:0{scoreMin}";
                else
                    finalString += $"0:{scoreMin}";

                if (restSeg < 10)
                    finalString += $":0{restSeg}";
                else
                    finalString += $":{restSeg}";
            }
            else
            {
                int scoreHours = scoreSeg / 3600;
                int scoreMin = scoreSeg / 60;
                int restSeg = scoreSeg % 60;

                if (scoreHours < 10)
                    finalString += $"0{scoreHours}";
                else
                    finalString += $"{scoreHours}";

                if (scoreMin < 10)
                    finalString += $":0{scoreMin}";
                else
                    finalString += $":{scoreMin}";

                if (restSeg < 10)
                    finalString += $":0{restSeg}";
                else
                    finalString += $":{restSeg}";
            }

            return finalString;
        }

        public int CompareSort(UserDto user1, UserDto user2)
        {
            if (user1.scoreSeg > user2.scoreSeg)
                return 1;
            else if (user1.scoreSeg < user2.scoreSeg)
                return -1;
            else
                return 0;
        }
    }
}