using UnityEditor;
using UnityEngine;
using Assets.Scripts.SceneManagers;

public class PlayerRenderer: MonoBehaviour
{
    public GameObject playersParent;

    public void RenderAllPlayersInScene(string scene)
    {
        foreach (Player player in CrossSceneInfo.PlayerList.Values)
        {

            if (player.Scene == scene && player.Id != CrossSceneInfo.MyId)
            {
                Render(player);
            }
        }
    }
    public void Render(Player player)
    {
        Debug.Log("RENDERING");
        Vector2 position = player.Position;

        GameObject newPlayerPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Prefabs/Player Prefabs/Player.prefan", typeof(GameObject));
        player.Prefab = newPlayerPrefab;

        //Spawn the new player inside "Players" GameObject
        GameObject newPlayerGameObject = Instantiate(newPlayerPrefab, position, Quaternion.identity, playersParent.transform);
        newPlayerGameObject.name = player.Username;
        player.GameObject = newPlayerGameObject;

        DebugMsg(player);

    }

    public void UnRender(Player player)
    {
        Destroy(player.GameObject);
        player.GameObject = null;
        player.Prefab = null;

        DebugMsg(player);

    }
    void DebugMsg(Player player)
    {
        #region Debug Messages
        if (player.Prefab == CrossSceneInfo.PlayerList[player.Id].Prefab)
        {
            Debug.Log("Prefabs are the same");

        }
        else
        {
            Debug.Log("Prefabs aren't the same");

        }
        if (player.GameObject == CrossSceneInfo.PlayerList[player.Id].GameObject)
        {
            Debug.Log("GameObjects are the same");

        }
        else
        {
            Debug.Log("GameObjects aren't the same");

        }
        #endregion
    }
}
