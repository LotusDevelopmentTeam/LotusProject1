using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Assets.Scripts.SceneManagers;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    GameObject MenuScreen;
    GameObject SettingsScreen;
    GameObject OnlineScreen;

    public GameObject ServerAddPrefab;
    public GameObject LoginPrefab;
    public GameObject Window;

    public List<string> servers;
    public List<string> ips;
    public List<string> ports;

    public List<Transform> ServerButtons;

    void Start()
    {
        init();
    }

    public void Campaign()
    {
        CrossSceneInfo.OnlineMode = false;
        CrossSceneInfo.InGame = true;
        SceneManager.LoadScene("MainScene");
    }
    public void Online()
    {
        MenuScreen.SetActive(false);
        OnlineScreen.SetActive(true);
        SettingsScreen.SetActive(false);
        RefreshServers();
    }
    public void Settings()
    {
        MenuScreen.SetActive(false);
        OnlineScreen.SetActive(false);
        SettingsScreen.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        MenuScreen.SetActive(true);
        OnlineScreen.SetActive(false);
        SettingsScreen.SetActive(false);
    }

    public void AddServer()
    {
        Window = Instantiate(ServerAddPrefab, GameObject.Find("CenterOfScreen").transform);
    }

    public void Login()
    {
        Window = Instantiate(LoginPrefab, GameObject.Find("CenterOfScreen").transform);
    }

    void init()
    {
//      Get the GameObjects
        MenuScreen = GameObject.Find("MenuScreen");
        SettingsScreen = GameObject.Find("SettingsScreen");
        OnlineScreen = GameObject.Find("OnlineScreen");

//      Enable the right UI
        MenuScreen.SetActive(true);
        OnlineScreen.SetActive(false);
        SettingsScreen.SetActive(false);

//      Retrieve player preferences
        DpkgPrefs(PlayerPrefs.GetString("Servers"));

        //      Get the server buttons
        for (int i = 0; i < OnlineScreen.transform.Find("Servers").childCount; i++)
        {
            ServerButtons.Add(OnlineScreen.transform.Find("Servers").GetChild(i));
        }
    }

    void DpkgPrefs(string prefs)
    {
        //Ej. Jake's Server,192.168.1.1,8080|Public Server 1,192.168.1.104,40|Dead Server,192.168.1.32,9529
        string[] split_prefs = prefs.Split('|');
        servers.Clear();
        ips.Clear();
        ports.Clear();
        for (int i = 1; i < split_prefs.Length; i++)
        {
            servers.Add(split_prefs[i].Split(',')[0]);
            ips.Add(split_prefs[i].Split(',')[1]);
            ports.Add(split_prefs[i].Split(',')[2]);
        } 
    }

    public void RefreshServers()
    {
        DpkgPrefs(PlayerPrefs.GetString("Servers"));

        for (int i = 0; i < ServerButtons.Count; i++)
        {
            ServerButtons[i].gameObject.SetActive(true);        
        }

        for (int i = 0; i < ServerButtons.Count; i++)
        {
            if (i < servers.Count)
            {
                ServerButtons[i].Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = servers[i];
            }
            else
            {
                ServerButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
