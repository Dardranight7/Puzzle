using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMaxScoreModel : MonoBehaviour
{
    private const string baseUrl = "https://puzzlebatman-default-rtdb.firebaseio.com/MaxScore";
    public void GetMaxScore()
    {
        FirebaseRequest.FirebaseObjectRequestPetiton<int>(baseUrl,null,
            null,
            (success, maxScore) =>
            {
                Debug.Log(maxScore);
            },
            RequestType.GET);
    }

    [ContextMenu("SetMaxScore")]
    public void SetMaxScore()
    {
        FirebaseRequest.FirebaseObjectRequestPetiton<MaxScorePayload>(baseUrl,null,
            new MaxScorePayload(){MaxScore = 3 },
            (success, maxScore) =>
            {
                Debug.Log(maxScore.MaxScore);
            },
            RequestType.PATCH);
    }

    public void DeleteMaxScore()
    {
        FirebaseRequest.FirebaseObjectRequestPetiton<int>(baseUrl,null,
            null,
            (success, maxScore) =>
            {
                Debug.Log(maxScore);
            },
            RequestType.DELETE);
    }
}
public class MaxScorePayload
{
    public int MaxScore;
}
