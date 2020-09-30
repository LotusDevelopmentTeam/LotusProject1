using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerName : MonoBehaviour
{
    GameObject serverName;
    void Start()
    {
        serverName = GameObject.Find("ServerName");
        serverName.GetComponent<TextMeshProUGUI>().text = CrossSceneInfo.ServerName;
    }

}
