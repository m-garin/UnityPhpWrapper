using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace ClientServerDataExchange
{
    public class UnityWebRequestWrapper
    {
		public static bool cacheBusting = true; //if you want to be sure exactly which queries are not cached

        public static void Call(string _serverLink, MonoBehaviour _monoBehaviour, Dictionary<string, string> _parameters, Callback _CallbackSuccess)
        {
            _monoBehaviour.StartCoroutine(Upload(_serverLink, _parameters, _CallbackSuccess, DefaultCallback));
        }

        public static void Call(string _serverLink, MonoBehaviour _monoBehaviour, Dictionary<string, string> _parameters, Callback _CallbackSuccess, Callback _CallbackError)
        {
            _monoBehaviour.StartCoroutine(Upload(_serverLink, _parameters, _CallbackSuccess, _CallbackError));
        }

        public delegate void Callback(UnityWebResponseWrapper _response);

        private static void DefaultCallback(UnityWebResponseWrapper _response)
        {
            //Debug.Log(_response.text);
        }

        private static IEnumerator Upload(string _serverLink, Dictionary<string, string> _parameters, Callback _CallbackSuccess, Callback _CallbackError)
        {
            UnityWebRequest www;

            if (_parameters == null || _parameters.Count == 0)
            {
                //Debug.Log("GET");

                if (UnityWebRequestWrapper.cacheBusting)
                {
                    _serverLink += "?t=" + (System.Int32)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds; // + UnixTime for break query cache 
                }
                www = UnityWebRequest.Get(_serverLink);
            }
            else
            {
                //Debug.Log("POST");

                WWWForm form = new WWWForm();
                foreach (var pair in _parameters)
                {
                    form.AddField(pair.Key, pair.Value);
                }

                www = UnityWebRequest.Post(_serverLink, form);
            }

            yield return www.Send();

            UnityWebResponseWrapper response = new UnityWebResponseWrapper(www.downloadHandler.data, www.downloadHandler.text);
            if (www.isNetworkError)
            {
                _CallbackError(response);
            }
            else
            {
                //Debug.Log(response.text);
                _CallbackSuccess(response);
            }
        }
    }
}
