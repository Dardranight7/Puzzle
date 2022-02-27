using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject VentanaData, VentanaInstruccion;
    bool readInstructions = true;
    float tiempoIncial;
    private void Start()
    {
        tiempoIncial = Time.time;
    }

    public void SetTiempoInicial()
    {
        tiempoIncial = Time.time;
        StartCoroutine(ApagarVentana());
    }

    public void ModoJuego()
    {
        
    }

    private void Update()
    {
        float valorTiempo = Time.time - tiempoIncial;
        float minutos = (int)valorTiempo / 60;
        float segundos = valorTiempo - (minutos * 60);
        //uiManager.SetTextoTiempo(minutos.ToString("00") + ":" + segundos.ToString("00"));
        GameData.gameData.SetTime(valorTiempo);
    }

    public void QuitInstructions()
    {
        readInstructions = false;
    }

    IEnumerator ApagarVentana()
    {
        VentanaData.SetActive(false);
        VentanaInstruccion.SetActive(true);
        while (readInstructions)
        {
            yield return null;
        }
        VentanaInstruccion.SetActive(false);
        tiempoIncial = Time.time;
    }
}
