using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

namespace Puzzle.UserData
{
    public class FormUserDataController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_InputField lastNameInputField;
        [SerializeField] private TMP_InputField cedulaInputField;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField cityInputField;

        public void SubmitForm()
        {
            ExampleUserModel.SENDNewUser(new UserDto()
            {
                nombre = nameInputField.text,
                apellido = lastNameInputField.text,
                cedula = cedulaInputField.text,
                email = emailInputField.text,
                ciudad = cityInputField.text,
                scoreMin = 5,
                scoreSeg = 5
            });
        }

        [ContextMenu("Get users")]
        public void Get()
        {
            ExampleUserModel.GETUsers(RequestUsersCallback);
        }

    public static void RequestUsersCallback(bool success, FirebaseListDto<UserDto> data)
    {
        Debug.Log($"Success: {success}");

        if (success)
            Debug.Log(JsonConvert.SerializeObject(data));
    }
    }
}