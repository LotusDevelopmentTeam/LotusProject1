using Assets.Scripts.SceneManagers;
using System.Collections.Generic;
using UnityEngine;

public static class CrossSceneInfo
{
    private static bool inGame = false;
    public static bool OnlineMode { get; set; }
    public static Vector2 SpawnPoint { get; set; }
    public static int OnlinePlayers { get; set; }
    public static bool InGame
    {
        get
        {
            return inGame;
        }
        set
        {
            inGame = value;
        }
    }
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
