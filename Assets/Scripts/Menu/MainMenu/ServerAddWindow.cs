using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerAddWindow : MonoBehaviour
{

    Main_Menu mainScript;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Address;
    public TextMeshProUGUI Port;


    // Start is called before the first frame update
    void Start()
    {
        mainScript = GameObject.Find("MenuManager").GetComponent<Main_Menu>();
        Name = transform.Find("Inputs").Find("Name").Find("Text Area").Find("Text").GetComponent<TextMeshProUGUI>();
        Address = transform.Find("Inputs").Find("Address").Find("Text Area").Find("Text").GetComponent<TextMeshProUGUI>();
        Port = transform.Find("Inputs").Find("Port").Find("Text Area").Find("Text").GetComponent<TextMeshProUGUI>();
    }    

    public void Add()
    {
        PlayerPrefs.SetString("Servers", (PlayerPrefs.GetString("Servers") + "|"+ Name.text + "," + Address.text + "," + Port.text));
        mainScript.RefreshServers();
        Destroy(gameObject);
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
