using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class SendServerValues : MonoBehaviour
{

    Main_Menu mainScript;


    // Start is called before the first frame update
    void Start()
    {
        mainScript = GameObject.Find("MenuManager").GetComponent<Main_Menu>();
    }

    // Update is called once per frame
    public void SetValues()
    {
        string Name = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text;
        int index = mainScript.servers.IndexOf(Name);
        Server.Ip = mainScript.ips[index];
        Server.Port = Int32.Parse(mainScript.ports[index].Remove(mainScript.ports[index].Length-1));
        mainScript.Login();
        Debug.Log(Server.Ip);
        Debug.Log(Server.Port);
    }
}
