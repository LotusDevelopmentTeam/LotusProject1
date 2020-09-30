using Assets.Scripts.SceneManagers;
using System.Globalization;
using UnityEngine;

public class SceneChangeManager : ManagerBase
{
    public override void Response(Packet packet)
    {
        PlayerRenderer renderer = new PlayerRenderer();
        string myId = CrossSceneInfo.MyId;
        string id = packet.Content["ID"];
        string scene = packet.Content["SCENE"];
        float pos_x = float.Parse(packet.Content["POS_X"], CultureInfo.InvariantCulture.NumberFormat);
        float pos_y = float.Parse(packet.Content["POS_Y"], CultureInfo.InvariantCulture.NumberFormat);
        Vector2 position = new Vector2(pos_x, pos_y);



        if (id == myId)
        {
            CrossSceneInfo.PlayerList[myId].Scene = scene;
            renderer.RenderAllplayersInScene(scene);
        }

        // If player comes to my scene
        else if (scene == CrossSceneInfo.PlayerList[myId].Scene)
        {
            renderer.Render(CrossSceneInfo.PlayerList[id], position);
        }
        else
        {
            renderer.UnRender(CrossSceneInfo.PlayerList[id]);
        }
    }
}
