using System;
using UnityEngine;
public static class ExampleUserModel
{
    private const string baseUrl = "https://puzzlebatman-default-rtdb.firebaseio.com/Users";

    public static void GETUsers(Action<bool, FirebaseListDto<UserDto>> callback)
    {
        FirebaseRequest.FirebaseListRequestPetiton<UserDto>(baseUrl, null, callback, RequestType.GET);
    }

    [ContextMenu("New user")]
    public static void SENDNewUser(UserDto userData)
    {
        FirebaseRequest.FirebaseListRequestPetiton<UserDto>(baseUrl, userData,
        (success, data) =>
        {
            if (success)
            {
                Debug.Log(success);
            }
        },
        RequestType.PATCH);
    }

    public static void DELETEUser()
    {
        FirebaseRequest.FirebaseListRequestPetiton<UserDto>(baseUrl, 1, null, RequestType.DELETE);
    }


}
