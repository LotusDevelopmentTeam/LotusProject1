using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public InputField username_field;
    public InputField password_field;
    public InputField ip_field;  

    public void SinglePlayer()
    {
        CrossSceneInfo.OnlineMode = false;
        SceneManager.LoadScene("SampleScene");
    }
    public void Multiplayer()
    {
        CrossSceneInfo.OnlineMode = true;
        
        SceneManager.LoadScene("SampleScene");
    }
}
