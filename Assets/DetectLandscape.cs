using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLandscape : MonoBehaviour
{
    [SerializeField] GameObject advice;
    void Update()
    {
        advice.SetActive(Screen.width < Screen.height);
    }
}
