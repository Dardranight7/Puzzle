using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Puzzle.UserData
{
    public class FirebaseTokenManager : MonoBehaviour
    {
        public string tokenFirebase = "";
        private int tokenExpirationTime = 3600;
        private float initTimeTokenFirebase;

        public static FirebaseTokenManager instance;
        private void Awake()
        {
            //Singleton
            if (instance != null)
                Destroy(this.gameObject);

            instance = this;
            DontDestroyOnLoad(this);

            Init();
        }

        public void Init()
        {
            initTimeTokenFirebase = Time.time;
            StartCoroutine(RequestTokenFirebase());
        }

        private string pss = "8a,p6?uEfG!(Q^n";
        private void Update()
        {
            if (Time.time > initTimeTokenFirebase + tokenExpirationTime)
            {
                StartCoroutine(RequestTokenFirebase());
                Debug.Log("Reset Firebase Token");
                initTimeTokenFirebase = Time.time;
            }
        }

        IEnumerator RequestTokenFirebase()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get("https://pzzl-bat-ps.netlify.app/.netlify/functions/api"))
            {
                webRequest.SetRequestHeader("Access-Control-Allow-Credentials", "true");
                webRequest.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
                webRequest.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
                webRequest.SetRequestHeader("X-Requested-With", "https://arjs-cors-proxy.herokuapp.com/");
                yield return webRequest.SendWebRequest();
                if (webRequest.isNetworkError)
                {
                    Debug.LogError("Error: " + webRequest.error);
                }
                else
                {
                    if (!String.IsNullOrEmpty(webRequest.downloadHandler.text))
                        tokenFirebase = DecryptStringWithXORFromHex(webRequest.downloadHandler.text.Replace("\"", ""), pss);
                    else
                        StartCoroutine(RequestTokenFirebase());
                }
            }
        }

        private string DecryptStringWithXORFromHex(string input, string key)
        {
            StringBuilder c = new StringBuilder();
            while ((key.Length < (input.Length / 2)))
                key += key;

            for (int i = 0; i < input.Length; i += 2)
            {
                string hexValueString = input.Substring(i, 2);
                int value1 = Convert.ToByte(hexValueString, 16);
                int value2 = key[i / 2];
                int xorValue = (value1 ^ value2);
                c.Append(Char.ToString(((char)(xorValue))));
            }

            return c.ToString();
        }
    }
}