using Assets.Scripts.SceneManagers;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginManager : ManagerBase
{
    public Button login_signup_button;
    public static Text result_txt;

    string username;
    string password;



    public override void Response(Packet packet)
    {
        
        string mode = packet.Content["MODE"];
        bool result = !packet.Content.ContainsKey("MSG");

        if (result)
        {
            if (mode == Packet.SIGNUP_MODE)
            {
            result_txt.text = "Signed Up successfuly, please log in";
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

        else
        {
            result_txt.text = packet.Content["MSG"];
        }

        login_signup_button.enabled = true;
    }


    public void LogIn(string username, string password)
    {
        login_signup_button.enabled = false;
        Packet packet = new Packet(username, password, Packet.LOGIN_MODE);
        Send(packet);
    }

    public void SignUp(string username, string password)
    {
        login_signup_button.enabled = false;
        Packet packet = new Packet(username, password, Packet.SIGNUP_MODE);
        Send(packet);

    }
}
