using System.Collections;
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

        private void Init()
        {
            initTimeTokenFirebase = Time.time;
            StartCoroutine(RequestTokenFirebase());
        }

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
                yield return webRequest.SendWebRequest();
                if (webRequest.isNetworkError)
                {
                    Debug.LogError("Error: " + webRequest.error);
                }
                else
                {
                    tokenFirebase = webRequest.downloadHandler.text.Replace("\"", "");
                }
            }
        }
    }
}