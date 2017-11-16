using System.Collections.Generic;
using UnityEngine;

namespace ClientServerDataExchange.PHP {
    public sealed class PhpWrapper : MonoBehaviour {

        private static PhpWrapper instance = null;
		public static string serverUrl; //web server adress. Example: http://localhost/game.php

        void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        void Start()
        {
            instance = this;
        }

        public delegate void Callback(Dictionary<string, object> _response);

        /// <summary>
        /// Calling a Server-Side
        /// </summary>
        /// <param name="_scriptId">script ID for php router</param>
        /// <param name="_parameters">Параметры, передаваемые серверу в $_POST</param>
        /// <param name="_Callback"></param>
        public static void Call (int _scriptId, Dictionary<string, string> _parameters, Callback _Callback)
        {

            if (_parameters.ContainsKey("i"))
            {
                Debug.LogError("Parameter 'i' is reserved");
                return;
            }

            _parameters.Add("i", _scriptId.ToString());

			UnityWebRequestWrapper.Call(serverUrl, instance, _parameters, (UnityWebResponseWrapper _data) => {
                _Callback(_data.json);
            });
        }
    }
}
