using UnityEditor;
using UnityEngine;
using Assets.Scripts.SceneManagers;

public class PlayerRenderer: MonoBehaviour
{
    public GameObject playersParent;
    public GameObject playerPrefab;

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

        //Spawn the new player inside "Players" GameObject
        GameObject newPlayerGameObject = Instantiate(playerPrefab, position, Quaternion.identity, playersParent.transform);
        
        newPlayerGameObject.name = player.Username;
        //TODO Get player prefab properties like clothes, equipment with player.Prefab
        // and change the GameObject texture to match player

        player.GameObject = newPlayerGameObject;

    }

    public void UnRender(Player player)
    {
        Destroy(player.GameObject);
        player.GameObject = null;
        player.Prefab = null;

    }

}
