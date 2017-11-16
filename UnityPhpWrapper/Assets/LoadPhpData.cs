using ClientServerDataExchange.PHP;
using System.Collections.Generic;
using UnityEngine;

public class LoadPhpData : MonoBehaviour {

    // Use this for initialization
    void Start () {
        PhpWrapper.serverUrl = "http://localhost/game.php";  //web server adress. Example: http://localhost/game.php
    }

    public void ButtonReqAction ()
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("testRequest", "test msg"); //pass parameters into a php script
        int scriptId = 0; //for php router
        PhpWrapper.Call(scriptId, parameters, (Dictionary<string, object> _data) =>
        {
            Debug.Log(_data);
        });
    }
}
