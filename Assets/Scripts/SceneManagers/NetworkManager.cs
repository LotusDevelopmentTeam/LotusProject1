using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.SceneManagers;
using System;

public class NetworkManager : MonoBehaviour
{
    //Server object setup
    public static string server_address;
    public static int server_port;
    public Packet packet;

    // Manager Scipts setup
    #region Managers Set-Up
    public LoginManager loginManager;
    public PlayersConnectionsManager playersConnectionsManager;
    public PlayerMovementManager PlayerMovementManager;
    public SceneChangerManager sceneChangerManager;


    #endregion



    void Start()
    {
        if (CrossSceneInfo.InGame)
        {
            sceneChangerManager.Start();
        }

    }

    void Update()
    {
        if (Server.isConnected())
        {
            try
            {
                packet = Server.GetPacket();
            }
            catch (Exception ex)
            {
                if (!CrossSceneInfo.InGame)
                {
                    loginManager.EnableButtons();
                }

                Debug.Log(ex.Message);
            }

            #region Packet management
            if (packet.Type != Packet.NOT_VALID_TYPE)
            {
                //Call corresponding script based on packet Type
                if (CrossSceneInfo.InGame)
                {
                    if (CrossSceneInfo.OnlineMode)
                    {
                        ManageInGamePacket(packet);
                    }
                    else
                    {
                        // Save Offline stuff
                    }
                }
                else
                {
                    ManageMenuPacket(packet);
                }

            }
            #endregion

        }
    }

    private void ManageInGamePacket(Packet packet)
    {
        switch (packet.Type)
        {
            case Packet.LOGIN_SUCCESSFULY_TYPE:
                playersConnectionsManager.Response(packet);
                break;

            case Packet.DISCONNECT_TYPE:
                playersConnectionsManager.Response(packet);
                break;

            case Packet.PLAYER_SWITCH_SCENE:
                sceneChangerManager.Response(packet);
                break;

            case Packet.PLAYER_MOVEMENT_TYPE:
                //TODO
                break;

            case Packet.PLAYER_ANIMATION_TYPE:
                //TODO
                break;

            default:
                break;
        }
    }

    private void ManageMenuPacket(Packet packet)
    {
        switch (packet.Type)
        {
            case Packet.LOGIN_REGISTER_ERROR_TYPE:
                loginManager.Response(packet);
                break;

            case Packet.LOGIN_SUCCESSFULY_TYPE:
                loginManager.Response(packet);
                break;

            case Packet.REGISTER_SUCCESSFULY_TYPE:
                loginManager.Response(packet);
                break;

            default:
                break;
        }

    }

    public static void SendPacket(Packet packet)
    {
        Server.SendPacket(packet);
    }

    /*
    void AnalyzePackets(string temp)
    {
        print(temp);
        if (Accepted)
        {
            if (!temp.Contains("="))
            {
                try
                {
                    GameObject.Find((temp.Split(':')[0])).GetComponent<Player2Pos>().AnalyzePackets(temp.Split(':')[1]);
                    Player.GetComponent<InventorySystem>().AnalyzeMsg(temp);
                }
                catch
                {
                    EnemyPlayer = (GameObject)Instantiate(ClientPlayer, SpawnPoint, Quaternion.identity);
                    EnemyPlayer.name = temp.Split(':')[0];
                    EnemyPlayer.GetComponent<InventorySystem>().AnalyzeMsg(temp);
                }
            }

            if (temp.Contains("="))
            {
                string[] command = temp.Split('=');
                if (command[0] == "Join")
                {
                    EnemyPlayer = (GameObject)Instantiate(ClientPlayer, SpawnPoint, Quaternion.identity);
                    EnemyPlayer.name = command[1];
                }
                if (command[0] == "Pos")
                {
                    Vector2 receivedPosition = new Vector2(float.Parse(command[1].Split('a')[0]), float.Parse(command[1].Split('a')[1]));
                    if (Player != null)
                    {
                        Player.position = receivedPosition;
                    }
                    else
                    {
                        Player = Instantiate(HostPlayer, receivedPosition, Quaternion.identity).transform;
                    }
                }
                if (command[0] == "Items")
                {
                    Player.GetComponent<InventorySystem>().ReceiveItems(command[1]);
                }
            }
        }
        else
        {
            if (temp != "Accepted")
            {
                string[] values = temp.Split('|');
                GameObject Loaded = Instantiate(Items[int.Parse(values[0])], new Vector2(float.Parse(values[1]), float.Parse(values[2])), Quaternion.identity);
                Loaded.transform.name = values[0];
                if (Loaded.transform.tag == "PickUp")
                {
                    Loaded.transform.GetChild(0).transform.name = values[3];
                }
            }
            else
            {
                Accepted = true;
            }
        }

    }
    */
}