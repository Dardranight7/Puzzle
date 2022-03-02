using Puzzle.UserData;
using System;
using UnityEngine;
public static class UserModel
{
    private const string baseUrl = "https://puzzlebatman-default-rtdb.firebaseio.com/Users";

    public static void GETUsers(Action<bool, FirebaseListDto<UserDto>> callback)
    {
        Debug.Log("im here");
        FirebaseRequest.FirebaseListRequestPetiton<UserDto>(baseUrl, _callback: callback, _type: RequestType.GET);
    }

    public static void SENDNewUser(UserDto userData, Action callback)
    {
        if (String.IsNullOrEmpty(FirebaseTokenManager.instance.tokenFirebase))
            return;

        FirebaseRequest.FirebaseListRequestPetiton<UserDto>(baseUrl, FirebaseTokenManager.instance.tokenFirebase, userData,
        (success, data) =>
        {
            if (success)
            {
                callback?.Invoke();
            }
        },
        RequestType.PATCH);
    }
}
