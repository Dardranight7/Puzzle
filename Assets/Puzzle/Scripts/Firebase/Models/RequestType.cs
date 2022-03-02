using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using UnityEngine;
public enum RequestType
{
    GET,
    POST,
    PUT,
    PATCH,
    DELETE
}