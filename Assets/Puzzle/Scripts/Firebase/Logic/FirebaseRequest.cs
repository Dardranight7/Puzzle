using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class FirebaseRequest
{
    #region  Methods
    #region SingleObject Request 
    public static async void FirebaseObjectRequestPetiton<T>(string _url, string authToken = null, object _payload = null, Action<bool, T> _callback = null, RequestType _type = RequestType.GET)
    {
        StringContent data = null;
        if (_payload != null)
        {
            string json = JsonConvert.SerializeObject(_payload);
            data = new StringContent(json, Encoding.UTF8, "application/json");


            if (_type == RequestType.PATCH)
            {
                _url = _url.Remove(_url.LastIndexOf("/") + 1, _url.Length - (_url.LastIndexOf("/") + 1));
            }

            _url += ".json";
            if (authToken != null)
                _url += $"?auth={authToken}";

            if (_type == RequestType.GET)
                _url += SetGetParameters(json);
        }
        else
        {
            _url += ".json";
            if (authToken != null)
                _url += $"?auth={authToken}";
        }


        using (var httpClient = new HttpClient())
        {
            HttpResponseMessage response = new HttpResponseMessage();
            switch (_type)
            {
                case RequestType.GET:
                    response = await httpClient.GetAsync(_url);
                    break;
                case RequestType.POST:
                    response = await httpClient.PostAsync(_url, data);
                    break;
                case RequestType.PUT:
                    response = await httpClient.PutAsync(_url, data);
                    break;
                case RequestType.PATCH:
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), _url);
                    request.Content = data;
                    response = await httpClient.SendAsync(request);
                    break;
                case RequestType.DELETE:
                    response = await httpClient.DeleteAsync(_url);
                    break;
            }
            string result = response.Content.ReadAsStringAsync().Result;
            result = result.Replace("\\", string.Empty);

            T responseR = default(T);
            if (!String.IsNullOrEmpty(result) && result != "null")
                responseR = JsonConvert.DeserializeObject<T>(result);

            if (_callback != null)
            {
                if (IsAnyNotNullOrEmpty(responseR))
                    _callback(responseR != null, responseR);
                else
                    _callback(false, default(T));
            }
        }
    }
    #endregion SingleObject Request 

    #region List Request Petiton
    public static async void FirebaseListRequestPetiton<T>(string _url, string authToken = null, object _payload = null, Action<bool, FirebaseListDto<T>> _callback = null, RequestType _type = RequestType.GET, bool patchWithFinalPayload = false)
    {
        Debug.Log("i am start firebaseRequest Petition");
        StringContent data = null;
        Debug.Log("String content amiguitos");
        Debug.Log(_payload);
        Debug.Log("tryied to print payload");
        if (_payload != null)
        {
            Debug.Log(" Enter to if conditional");
            string json = "";
            if (_payload.GetType() == typeof(string))
            {
                Debug.Log(" i will payload");
                json = (string)_payload;
            }
            else
            {
                Debug.Log("I will use jsonConvert");
                json = JsonConvert.SerializeObject((_payload));
            }



            if (_type == RequestType.PATCH)
            {
                if (!patchWithFinalPayload)
                {
                    SetPayloadPatchRequest(_url, authToken, json, (finalJson) =>
                       {
                           FirebaseListRequestPetiton<T>(_url, authToken, finalJson, _callback, _type, true);
                       });
                    return;
                }
                else
                {
                    if (_payload.GetType() != typeof(ChildCountPayload))
                        _url += "/List";
                }
            }
            else if (_type == RequestType.DELETE)
            {
                LowerChildCount(_url, authToken);
                _url += $"/List/{(int)_payload}";
            }


            _url += ".json";
            if (authToken != null)
                _url += $"?auth={authToken}";

            data = new StringContent(json, Encoding.UTF8, "application/json");
            if (_type == RequestType.GET)
                _url += SetGetParameters(json);
        }
        else
        {
            _url += ".json";
            if (authToken != null)
                _url += $"?auth={authToken}";
        }


        using (var httpClient = new HttpClient())
        {
            HttpResponseMessage response = new HttpResponseMessage();
            switch (_type)
            {
                case RequestType.GET:
                    response = await httpClient.GetAsync(_url);
                    break;
                case RequestType.POST:
                    response = await httpClient.PostAsync(_url, data);
                    break;
                case RequestType.PUT:
                    response = await httpClient.PutAsync(_url, data);
                    break;
                case RequestType.PATCH:
                    var request = new HttpRequestMessage(new HttpMethod("PATCH"), _url);
                    request.Content = data;
                    response = await httpClient.SendAsync(request);
                    break;
                case RequestType.DELETE:
                    response = await httpClient.DeleteAsync(_url);
                    break;
            }
            Debug.Log("i trying to call awaits");
            string result = response.Content.ReadAsStringAsync().Result;
            result = result.Replace("\\", string.Empty);

            FirebaseListDto<T> responseR = default(FirebaseListDto<T>);
            if (!String.IsNullOrEmpty(result) && result != "null")
            {
                responseR = JsonConvert.DeserializeObject<FirebaseListDto<T>>(result);
                if (responseR != null && responseR.List != null)
                    responseR.List.RemoveAll(item => item == null);
            }


            if (_callback != null)
            {
                if (_type == RequestType.PATCH && patchWithFinalPayload && _payload.GetType() != typeof(ChildCountPayload))
                {
                    if (!String.IsNullOrEmpty(result) && result != "null")
                        _callback(true, null);
                    else
                        _callback(false, null);
                }
                else
                {
                    if (IsAnyNotNullOrEmpty(responseR))
                        _callback(responseR != null, responseR);
                    else
                        _callback(false, default(FirebaseListDto<T>));
                }
            }
            Debug.Log("all are fine");
        }
    }

    #region Patch Payload Set
    private static void SetPayloadPatchRequest(string _url, string authToken, string _originalJson, Action<string> _finalPayloadCallback)
    {
        string dq = ('"' + "");
        GetNumberOfListChilds(_url, authToken, (number) =>
          {
              SetNumberOfListChilds(_url, authToken, number + 1);

              string finalJson = "{" + dq + number + dq + ":" + _originalJson + "}";
              _finalPayloadCallback?.Invoke(finalJson);
          });
    }

    public static void GetNumberOfListChilds(string _url, string authToken, Action<int> _response)
    {
        FirebaseObjectRequestPetiton<int>(_url + "/ChildCount", authToken, _callback: (success, data) =>
           {
               if (success)
               {
                   _response?.Invoke(data);
               }
               else
               {
                   SetNumberOfListChilds(_url, authToken, 0);
                   _response.Invoke(0);
               }
           }, _type: RequestType.GET);
    }

    private static void SetNumberOfListChilds(string _url, string authToken, int _newChildCount)
    {
        FirebaseListRequestPetiton<ChildCountPayload>(
            _url, authToken,
            new ChildCountPayload() { ChildCount = _newChildCount }, null,
            RequestType.PATCH, true
        );
    }
    #endregion Patch Payload Set

    #region Delete Set
    public static void LowerChildCount(string _url, string authToken)
    {
        GetNumberOfListChilds(_url, authToken, (number) =>
          {
              SetNumberOfListChilds(_url, authToken, number - 1);
          });
    }
    #endregion Delete Set
    #endregion List Request Petiton

    #region Helpers
    private static string SetGetParameters(string json)
    {
        string paramsUrl = "";
        Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        foreach (KeyValuePair<string, object> entry in dictionary)
        {
            if (paramsUrl.Length > 0)
                paramsUrl += "&";
            paramsUrl += $"{entry.Key}={entry.Value}";
        }
        return $"?{paramsUrl}";
    }

    private static bool IsAnyNotNullOrEmpty(object myObject)
    {
        bool anyParamNotNull = true;
        foreach (FieldInfo pi in myObject.GetType().GetRuntimeFields())
        {
            if (pi.GetValue(myObject) == null)
            {
                anyParamNotNull = false;
            }
        }
        return anyParamNotNull;
    }
    #endregion Helpers
    #endregion  Methods
}
