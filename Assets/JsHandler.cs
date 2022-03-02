using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Puzzle.Scoreboard;

public class JsHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void PostResults(string str);

    [DllImport("__Internal")]
    private static extern void GetUser();

    [SerializeField] ScoreboardController scoreboardController;

    void Start()
    {
        scoreboardController = FindObjectOfType<ScoreboardController>(true);
    }

    public void EnviarDatos(string tiempo = "100")
    {
#if UNITY_EDITOR
        Debug.Log("Gano");
        return;
#endif
        //{\"puntaje\":\"100\"}
        PostResults(tiempo);
    }

    public void TomarDatos()
    {
#if UNITY_EDITOR
        scoreboardController.ReceiveUserDataFromOut("{\"-Mx83v-vwUPy9BmLCX9k\":{\"apellido\":\"Pardo Chaves\",\"cedula\":\"1098817967\",\"ciudad\":\"Bogota\",\"email\":\"pardo3@gmail.com\",\"nombre\":\"Daniel Andres\",\"scoreSeg\":5},\"-Mx83v9XHmcjc6tYI9I9\":{\"apellido\":\"Pardo Chaves\",\"cedula\":\"1098817967\",\"ciudad\":\"Bogota\",\"email\":\"pardo3@gmail.com\",\"nombre\":\"Daniel Andres\",\"scoreSeg\":5},\"-Mx83vIwZWOvpEsrRura\":{\"apellido\":\"Pardo Chaves\",\"cedula\":\"1098817967\",\"ciudad\":\"Bogota\",\"email\":\"pardo3@gmail.com\",\"nombre\":\"Daniel Andres\",\"scoreSeg\":5},\"-Mx83vYx2CPLiW9JX3LD\":{\"apellido\":\"Pardo Chaves\",\"cedula\":\"1098817967\",\"ciudad\":\"Bogota\",\"email\":\"pardo3@gmail.com\",\"nombre\":\"Daniel Andres\",\"scoreSeg\":5}}");
        return;
#endif
        GetUser();
    }
}
