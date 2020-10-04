using Assets.Scripts.SceneManagers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersConnectionsManager : ManagerBase
{
    public PlayerRenderer player_renderer;
    public override void Response(Packet packet)
    {
        string id = packet.Content["ID"];

        string myId = CrossSceneInfo.MyId;


        if (packet.Type == Packet.LOGIN_SUCCESSFULY_TYPE)
        {
            string username = packet.Content["USERNAME"];
            string scene = packet.Content["SCENE"];
            float pos_x = float.Parse(packet.Content["POS_X"], CultureInfo.InvariantCulture.NumberFormat);
            float pos_y = float.Parse(packet.Content["POS_Y"], CultureInfo.InvariantCulture.NumberFormat);
            int class_id = Convert.ToInt32(packet.Content["CLASS_ID"]);
            Vector2 position = new Vector2(pos_x, pos_y);

            if (!CrossSceneInfo.PlayerList.ContainsKey(id))
            {
                Player newPlayer = new Player(id, username, scene, pos_x, pos_y, class_id);
                if (scene == CrossSceneInfo.PlayerList[myId].Scene)
                {
                    player_renderer.Render(newPlayer);
                }
                CrossSceneInfo.PlayerList.Add(id, newPlayer);
            }

        }
        // Packet is a disconnection packet
        else
        {
            if (id == myId) // Kicked by server
            {
                CrossSceneInfo.Reset();
                SceneManager.LoadScene("Menu");
            }
            else
            {
                string scene = packet.Content["SCENE"];
                CrossSceneInfo.OnlinePlayers--;
                CrossSceneInfo.PlayerList.Remove(id);

                if (scene == CrossSceneInfo.PlayerList[myId].Scene)
                {
                    player_renderer.UnRender(CrossSceneInfo.PlayerList[id]);
                }
            }
        }

    }

    public void Disconnect()
    {
        string myId = CrossSceneInfo.MyId;
        Packet packet = new Packet(myId, true);
        Send(packet);
    }

}
