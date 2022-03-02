using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public JsHandler jsHandler;
    [SerializeField] Usuario usuario;
    [SerializeField] float tiempoJuego;
    bool cargar;

    public delegate void OnUserChangeHandler();
    public event OnUserChangeHandler OnUserChange;

    private void Awake()
    {
        if (gameData == null)
        {
            gameData = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (gameData != this)
            {
                GameData.gameData.jsHandler = FindObjectOfType<JsHandler>();
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        //GameData.gameData.jsHandler = FindObjectOfType<JsHandler>();
        //Debug.Log(JsonUtility.ToJson(usuario));
    }

    public void CargarFromJson(string data)
    {
        JsonUtility.FromJsonOverwrite(data, usuario);
        OnUserChange();
    }

    IEnumerator CargarConTiempo(bool wait)
    {
        if (true)
        {
            yield return new WaitForSeconds(5);
        }
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void SetTime(float tiempo)
    {
        tiempoJuego = tiempo;
    }

    public Usuario GetUsuario()
    {
        return usuario;
    }
}
