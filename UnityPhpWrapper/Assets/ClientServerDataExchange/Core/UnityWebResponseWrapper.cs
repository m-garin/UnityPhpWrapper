using MiniJSON;
using System.Collections.Generic;
using UnityEngine;

namespace ClientServerDataExchange
{
    public class UnityWebResponseWrapper
    {
        public string text { get; private set; }
        public byte[] data { get; private set; }
        public Dictionary<string, object> json
        {
            get
            {
                if (jsonLocal == null)
                {
                    try
                    {
                        jsonLocal = Json.Deserialize(text) as Dictionary<string, object>;
                    }
                    catch (System.OverflowException)
                    {
                        Debug.Log("Error: from server json");
                    }
                }
                return jsonLocal;
            }
        }
        private Dictionary<string, object> jsonLocal;

        public UnityWebResponseWrapper(byte[] _data, string _text)
        {
            this.text = _text;
            this.data = _data;
        }
    }
}