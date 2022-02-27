using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class JsHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void PostResults(string str);

    [DllImport("__Internal")]
    private static extern string GetUser();

    void Start()
    {

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
        GameData.gameData.CargarFromJson("{\"id\":11,\"nombre\":\"Luis Debug\",\"edad\":24}");
        return;
#endif
        //Debug.Log(GetUser());
        //GameData.gameData.CargarFromJson(GetUser());
    }
}
