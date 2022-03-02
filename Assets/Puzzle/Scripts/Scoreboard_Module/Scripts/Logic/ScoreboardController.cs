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
            //UserModel.GETUsers(RequestUsersCallback);
            jsHandler.TomarDatos();
        }

        public void ReceiveUserDataFromOut(string data)
        {
            //RequestUsersCallback(JsonConvert.DeserializeObject<List<UserDto>>(data));
            //string incomingJson = data.Split()
            var atun = JsonConvert.DeserializeObject<List<object>>(data);
            foreach (var item in atun)
            {
                Debug.Log(item);
            }
        }

        public void RequestUsersCallback(List<UserDto> data)
        {
            int counter = 1;
            data.Sort(CompareSort);

            foreach (var user in data)
            {
                RowScoreboardController row = Instantiate(scoreRowPrefab, scoreboardTable);
                row.Init($"{user.nombre} {user.apellido}", GiveTimeFormatter(user.scoreSeg), counter.ToString(), counter == 1);
                counter++;
            }
            Debug.Log(JsonConvert.SerializeObject(data));
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

                if (scoreSeg < 10)
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

                if (scoreSeg < 10)
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