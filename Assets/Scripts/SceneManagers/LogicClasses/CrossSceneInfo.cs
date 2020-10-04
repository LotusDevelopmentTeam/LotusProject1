using Assets.Scripts.SceneManagers;
using System.Collections.Generic;
using UnityEngine;

public static class CrossSceneInfo
{
    public static bool OnlineMode { get; set; }
    public static int OnlinePlayers { get; set; }
    public static string SceneName { get; set; }
    public static bool InGame { get; set; }
    public static string ServerName;

    public static string MyId { get; set; }
    public static Dictionary<string, Player> PlayerList { get; set; }


    public static void Reset()
    {
        OnlinePlayers = 0;
        InGame = false;
        PlayerList.Clear();
    }
}
