using Assets.Scripts.SceneManagers;
using System;
using System.Globalization;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginManager : ManagerBase
{

    Button send_button;
    Button back_button;
    public GameObject result_txt;



    public override void Response(Packet packet)
    {
        int type = Convert.ToInt32(packet.Content["TYPE"]);

        if (type == Packet.LOGIN_SUCCESSFULY_TYPE)
        {
            string myId = packet.Content["ID"];
            string username = packet.Content["USERNAME"];
            string scene = packet.Content["SCENE"];
            float pos_x = float.Parse(packet.Content["POS_X"], CultureInfo.InvariantCulture.NumberFormat);
            float pos_y = float.Parse(packet.Content["POS_Y"], CultureInfo.InvariantCulture.NumberFormat);
            int class_id = Convert.ToInt32(packet.Content["CLASS_ID"]);

            CrossSceneInfo.MyId = myId;
            Player me = new Player(myId, username, scene, pos_x, pos_y, class_id);
            CrossSceneInfo.PlayerList.Add(myId, me);
            CrossSceneInfo.OnlinePlayers++;
            CrossSceneInfo.SceneName = scene;
            SceneManager.LoadScene(scene);
        }
        else
        {
            string result_msg = packet.Content["MSG"];
            result_txt = GameObject.Find("Result_txt");
            result_txt.GetComponent<TextMeshProUGUI>().text = result_msg;
        }

        EnableButtons();
    }

    public void LogIn(string username, string password)
    {
        send_button = GameObject.Find("Confirm_btn").GetComponent<Button>();
        send_button.interactable = false;
        back_button = GameObject.Find("Back_btn").GetComponent<Button>();
        back_button.interactable = false;
        Packet packet = new Packet(username, password, Packet.LOGIN_MODE);
        Send(packet);
    }

    public void SignUp(string username, string password)
    {
        send_button = GameObject.Find("Confirm_btn").GetComponent<Button>();
        send_button.interactable = false;
        back_button = GameObject.Find("Back_btn").GetComponent<Button>();
        back_button.interactable = false;
        Packet packet = new Packet(username, password, Packet.REGISTER_MODE);
        Send(packet);

    }
    public void EnableButtons()
    {
        send_button = GameObject.Find("Confirm").GetComponent<Button>();
        send_button.interactable = true;
        back_button = GameObject.Find("Back").GetComponent<Button>();
        back_button.interactable = true;
    }

}
