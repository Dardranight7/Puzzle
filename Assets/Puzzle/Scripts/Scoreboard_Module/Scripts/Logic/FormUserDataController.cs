using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;
using System.Collections;

namespace Puzzle.UserData
{
    public class FormUserDataController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PuzzleManager puzzleManager;
        [SerializeField] private GameObject scoreBoardPanel;

        [Header("Input fields")]
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_InputField lastNameInputField;
        [SerializeField] private TMP_InputField cedulaInputField;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField cityInputField;

        [Header("Toggles")]
        [SerializeField] private Toggle toggleTyC;
        [SerializeField] private Toggle toggleDataTreatment;

        [Header("Labels")]
        [SerializeField] private TMP_Text errorMessage;

        public void SubmitForm()
        {
            if (!toggleTyC.isOn || !toggleDataTreatment)
            {
                errorMessage.gameObject.SetActive(true);
                errorMessage.text = "Acepta los términos y condiciones y el tratamiento de datos para continuar.";
                return;
            }

            if (String.IsNullOrEmpty(nameInputField.text) ||
                String.IsNullOrEmpty(lastNameInputField.text) ||
                String.IsNullOrEmpty(cedulaInputField.text) ||
                String.IsNullOrEmpty(emailInputField.text) ||
                String.IsNullOrEmpty(cityInputField.text))
            {
                errorMessage.gameObject.SetActive(true);
                errorMessage.text = "Completa todos los campos para registrar tu puntaje.";
                return;
            }

            UserModel.SENDNewUser(new UserDto()
            {
                nombre = nameInputField.text,
                apellido = lastNameInputField.text,
                cedula = cedulaInputField.text,
                email = emailInputField.text,
                ciudad = cityInputField.text,
                scoreSeg = 5
            }, () =>
             {
                 StartCoroutine(WaitForSeconds(0.5f, () =>
                 {
                     scoreBoardPanel.gameObject.SetActive(true);
                     this.gameObject.SetActive(false);
                 }));
             });
        }

        IEnumerator WaitForSeconds(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }

        private void OnDisable()
        {
            errorMessage.gameObject.SetActive(false);
        }
    }

}