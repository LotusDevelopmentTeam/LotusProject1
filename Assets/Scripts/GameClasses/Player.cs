using UnityEngine;

namespace Assets.Scripts.SceneManagers
{

    public class Player
    {

        public string Id { get; set; }
        public string Username { get; set; }
        public string Scene { get; set; }
        public Inventory Inventory { get; set; }
        public GameObject GameObject { get; set; }
        public GameObject Prefab { get; set; }
        public static string ActualScene { get; set; }
        public Vector2 Position;
        public Player(string id, string username, string scene = "Main", float pos_x = 0f, float pos_y = 0f)
        {
            Id = id;
            Username = username;
            Scene = scene;
            Position = new Vector2(pos_x, pos_y);
        }

    /*
     * CrossSceneInfo.Pos_x = float.Parse(packet.Content["POS_X"], CultureInfo.InvariantCulture.NumberFormat);
       CrossSceneInfo.Pos_y = float.Parse(packet.Content["POS_Y"], CultureInfo.InvariantCulture.NumberFormat);
    */
}
}
