using UnityEngine;

namespace Assets.Scripts.SceneManagers
{

    public class Player
    {

        public string Id { get; set; }
        public string Username { get; set; }
        public string Scene { get; set; }
        public int ClassId { get; set; }
        public Inventory Inventory { get; set; }
        public GameObject GameObject { get; set; }
        public GameObject Prefab { get; set; }

        public Vector2 Position;
        public Player(string id, string username, string scene, float pos_x, float pos_y, int class_id)
        {
            Id = id;
            Username = username;
            Scene = scene;
            Position = new Vector2(pos_x, pos_y);
            ClassId = class_id;
        }

    }
}
