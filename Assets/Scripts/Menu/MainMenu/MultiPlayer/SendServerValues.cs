using UnityEngine;
using TMPro;
using System;


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
        string name = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text;
        CrossSceneInfo.ServerName = name;
        int index = mainScript.servers.IndexOf(name);
        Server.Ip = mainScript.ips[index];
        Server.Port = Int32.Parse(mainScript.ports[index].Remove(mainScript.ports[index].Length-1));
        mainScript.ServerOptions();
    }
}
