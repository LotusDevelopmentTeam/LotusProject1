using Assets.Scripts.SceneManagers;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginManager : ManagerBase
{
    string username;
    Button send_button;
    Button back_button;
    public GameObject result_txt;



    public override void Response(Packet packet)
    {

        string mode = packet.Content["MODE"];
        bool result = !packet.Content.ContainsKey("MSG");

        if (result)
        {
            if (mode == Packet.REGISTER_MODE)
            {
                //result_txt.text = "Signed Up successfuly, please log in";
            }
            else
            {
                string myId = packet.Content["ID"];
                username = packet.Content["USERNAME"];
                string scene = packet.Content["SCENE"];
                float pos_x = float.Parse(packet.Content["POS_X"], CultureInfo.InvariantCulture.NumberFormat);
                float pos_y = float.Parse(packet.Content["POS_Y"], CultureInfo.InvariantCulture.NumberFormat);

                CrossSceneInfo.MyId = myId;
                Player me = new Player(myId, username, scene, pos_x, pos_y);
                CrossSceneInfo.PlayerList.Add(myId, me);
                SceneManager.LoadScene(scene);
            }
        }
        #region Couldn't Login/Signup. Show msg
        else
        {
            string result_msg = packet.Content["MSG"];
            result_txt = GameObject.Find("Result_txt");
            result_txt.GetComponent<TextMeshProUGUI>().text = result_msg;
        }
        #endregion

        send_button = GameObject.Find("Confirm").GetComponent<Button>();
        send_button.interactable = true;
        back_button = GameObject.Find("Back").GetComponent<Button>();
        back_button.interactable = true;

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
}
