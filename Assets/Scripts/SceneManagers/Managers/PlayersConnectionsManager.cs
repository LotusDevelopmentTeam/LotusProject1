using Assets.Scripts.SceneManagers;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersConnectionsManager : ManagerBase
{
    public GameObject playersParent;
    public Vector2 SpawnPoint = CrossSceneInfo.SpawnPoint;

    public override void Response(Packet packet)
    {
        string id = packet.Content["ID"];
        string username = packet.Content["USERNAME"];
        string scene = packet.Content["SCENE"];
        float pos_x = float.Parse(packet.Content["POS_X"], CultureInfo.InvariantCulture.NumberFormat);
        float pos_y = float.Parse(packet.Content["POS_Y"], CultureInfo.InvariantCulture.NumberFormat);
        Vector2 position = new Vector2(pos_x, pos_y);

        string myId = CrossSceneInfo.MyId;
        
        PlayerRenderer renderer = new PlayerRenderer();


        if (packet.Type == Packet.LOGIN_SUCCESSFULY_TYPE)
        {
            if (!CrossSceneInfo.PlayerList.ContainsKey(id))
            {
                Player newPlayer = new Player(id, username, scene);

                // If player login in my scene
                if (CrossSceneInfo.PlayerList[myId].Scene == scene)
                {
                    renderer.Render(newPlayer, position);
                }

                CrossSceneInfo.PlayerList.Add(id, newPlayer);
            }

        }
        // Packet is a disconnection packet
        else
        {
            if (id == myId)
            {
                //TODO CURRENTINVENTORY
                Player me = CrossSceneInfo.PlayerList[myId];

                packet = new Packet(id, me.Scene, me.Position, me.Inventory);
                CrossSceneInfo.Reset();
                SceneManager.LoadScene("Menu");
            }
            else
            {
                CrossSceneInfo.OnlinePlayers--;
                CrossSceneInfo.PlayerList.Remove(id);

                if (scene == CrossSceneInfo.PlayerList[myId].Scene)
                {
                    renderer.UnRender(CrossSceneInfo.PlayerList[id]);
                }
                
            }
        }

    }

    public void Disconnect (Player player)
    {

    }

}
