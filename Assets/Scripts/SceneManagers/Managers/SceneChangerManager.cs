using Assets.Scripts.SceneManagers;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerManager : ManagerBase
{
    public PlayerRenderer player_renderer;
    public override void Response(Packet packet)
    {
        string myId = CrossSceneInfo.MyId;
        string id = packet.Content["ID"];
        string scene = packet.Content["SCENE"];

        //TODO SpawnPoint for every SCENE
        CrossSceneInfo.PlayerList[id].Position = new Vector2(0f, 0f);


        // If player comes to my scene
        if (scene == CrossSceneInfo.PlayerList[myId].Scene)
        {
            player_renderer.Render(CrossSceneInfo.PlayerList[id]);
        }
        else
        {
            player_renderer.UnRender(CrossSceneInfo.PlayerList[id]);
        }
    }

    public void ChangeScene(string scene)
    {
        string myId = CrossSceneInfo.MyId;
        Packet packet = new Packet(scene, 0);
        Send(packet);
        CrossSceneInfo.SceneName = scene;
        CrossSceneInfo.PlayerList[myId].Scene = scene;
        SceneManager.LoadScene(scene);
    }
    public void Start()
    {
        player_renderer.RenderAllPlayersInScene(CrossSceneInfo.SceneName);

    }
}
