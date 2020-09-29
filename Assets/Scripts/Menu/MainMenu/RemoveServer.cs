using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemoveServer : MonoBehaviour
{

    Main_Menu mainScript;

    void Start()
    {
        mainScript = GameObject.Find("MenuManager").GetComponent<Main_Menu>();
    }

    public void DeleteServer()
    {
        // Get values from parent button
        string Name = transform.parent.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text;
        int index = mainScript.servers.IndexOf(Name);

        // Remove server from Player Preferences
        PlayerPrefs.SetString("Servers", PlayerPrefs.GetString("Servers").Replace(("|" + Name + "," + mainScript.ips[index] + "," + mainScript.ports[index]), ""));
        mainScript.servers.RemoveAt(index);
        mainScript.ips.RemoveAt(index);
        mainScript.ports.RemoveAt(index);

        // Refresh ServerList
        mainScript.RefreshServers();        
    }

}
