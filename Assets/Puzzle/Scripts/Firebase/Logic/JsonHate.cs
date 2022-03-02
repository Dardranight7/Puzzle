using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonHate : MonoBehaviour
{
    class IHateJsonConvert
    {
        public IHateJsonConvert(string catString)
        {
            gato = catString;
        }
        public string gato = "";
    }
    public void TestHate()
    {
        Debug.Log(JsonConvert.SerializeObject(new IHateJsonConvert("just a cat")));
    }
}
