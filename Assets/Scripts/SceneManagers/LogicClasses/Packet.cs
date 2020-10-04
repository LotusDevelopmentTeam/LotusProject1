using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Assets.Scripts.SceneManagers
{
    /*
     * Documentation for Packets
     * Packets formats without id are only sent by this Client to Server.
     * This client will never receive a packet without ID.
     * 
     * FORMATS:
     *      REGISTER / LOGIN FORMAT:                Packet(username, password, mode='r'/'l' )
     *      REGISTER / LOGIN ERROR:                 Packet(msg, mode='s'/'l')
     *      REGISTER_SUCCESSFULY FORMAT:            Packet(id, username)
     *      LOGIN_SUCCESFULY FORMAT:                Packet(id, username, scene, pos_x, pos_y)
     *      DISCONNECT FORMAT:                      Packet(id)
     *      PLAYER_SWITCH_SCENE FORMAT:             Packet(id, scene, pos_x, pos_y)
     *      PLAYER_MOVEMENT FORMAT:                 Packet(id, pos_x, pos_y)
     *      PLAYER_ANIMATION FORMAT:                Packet(id, animation_id)
     *      PLAYER_DROP_ITEM FORMAT:                Packet(id, item_id, amount, pos_x, pos_y)
     *      PLAYER_GRAB_ITEM FORMAT:                Packet(id, item_id, pos_x, pos_y)
     *      PLAYER_SHOOT FORMAT:                    Packet(id, shoot_id, speed, dir_x, dir_y)
     *      PLAYER_SHIELD FORMAT:                   Packet(id, shield_id)
     *      PLAYER_CHAT FORMAT:                     Packet(id, msg)
     *      PLAYER_CHANGE_SCENE FORMAT:             Packet(id, scene)
     *  
     */
    public class Packet
    {
        #region Format Vars

        public const int NOT_VALID_TYPE = 0;

        #region Modes
        public const string REGISTER_MODE = "s";
        public const string LOGIN_MODE = "l";
        public const string DISCONNECT_MODE = "d";
        #endregion

        #region Register / Login Packets


        public const int REGISTER_SUCCESSFULY_TYPE = -4;
        public const int LOGIN_REGISTER_ERROR_TYPE = -3;
        public const int LOGIN_REGISTER_TYPE = -1;
        public const int ID_PACKET = -2;
        public const int LOGIN_SUCCESSFULY_TYPE = 1;
        #endregion

        #region In Game Packets
        public const int DISCONNECT_TYPE = 3;
        public const int PLAYER_SWITCH_SCENE = 4;
        public const int PLAYER_MOVEMENT_TYPE = 5;
        public const int PLAYER_ANIMATION_TYPE = 6;
        public const int PLAYER_DROP_ITEM_TYPE = 7;
        public const int PLAYER_GRAB_ITEM_TYPE = 8;
        public const int PLAYER_BUILD_TYPE = 9;
        public const int PLAYER_SHOOT_TYPE = 10;
        public const int PLAYER_SHIELD_TYPE = 11;
        public const int PLAYER_CHAT_TYPE = 12;
        #endregion

        #region Special Chars
        public string END_MSG_CHAR = ";";
        public string DELIMITER_CHAR = ",";
        public string KEY_VALUE_CHAR = "=";
        #endregion


        #endregion

        #region Properties
        public string Msg { get; set; }
        public int Type { get; set; }
        public Dictionary<string, string> Content { get; set; }
        #endregion

        #region Constructors & Overloads
        /// <summary>
        /// Unkown packet.
        /// <para>This is usefull when packet comes from server</para>
        /// </summary>
        public Packet(string msg)
        {
            Msg = msg;
            Content = _get_content();
            Type = _get_type();

        }


        /// <summary>
        /// Register / Login packet.
        /// <para>mode = LOGIN_MODE or SIGNUP_MODE</para>
        /// </summary>
        public Packet(string username, string password, string mode = LOGIN_MODE)
        {
            Type = LOGIN_REGISTER_TYPE;
            Msg = "TYPE=" + Type.ToString() + ",USERNAME=" + username + ",PASSWORD=" + password + ",MODE=" + mode + ";";
            Content = _get_content();
        }



        /// <summary>
        /// Disconnect packet
        /// </summary>
        public Packet(string id, bool disconnect)
        {
            Type = DISCONNECT_TYPE;
            Msg = "TYPE=" + Type.ToString() + ",ID=" + id + END_MSG_CHAR;
            Content = _get_content();
        }

        public Packet(string scene, int change)
        {
            Type = PLAYER_SWITCH_SCENE;
            Msg = "TYPE=" + Type.ToString() + ",SCENE=" + scene + END_MSG_CHAR;
            Content = _get_content();
        
        }



        /// <summary>
        /// Movement packet
        /// <para>You will get one of this Packets only if you are in the same scene as the id's owner</para>
        /// </summary>
        public Packet(int id, Vector2 position)
        {
            Type = PLAYER_MOVEMENT_TYPE;
            Msg = "TYPE=" + Type.ToString() + _pos_to_text(position) + ";";
            Content = _get_content();
        }



        #endregion

        #region Methods
        private Dictionary<string, string> _get_content()
        {
            Dictionary<string, string> content = new Dictionary<string, string>();
            string msg = Msg;
            string[] splited_msg = msg.Remove(msg.Length - 1).Split(DELIMITER_CHAR.ToCharArray());

            foreach (string item in splited_msg)
            {
                string[] key_value = item.Split(KEY_VALUE_CHAR.ToCharArray());
                content.Add(key_value[0], key_value[1]);
            }
            return content;
        }

        private int _get_type()
        {
            // If Msg is incomplete
            if (Msg[Msg.Length - 1] != ';') return 0;

            string type = "0";
            Content.TryGetValue("TYPE", out type);
            return Convert.ToInt32(type);
        }

        private string _pos_to_text(Vector2 position)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            string str_pos = ",POS_X=" + position.x.ToString("F5", culture) + ",POS_Y=" + position.y.ToString("F5", culture);
            return str_pos;
        }

        private string _inventory_to_text(Inventory inventory)
        {
            //TODO Finish inventory system and inventory_to_text
            string str_inv = ",INVENTORY=" + "SOMETHING";
            return str_inv;
        }
        #endregion
    }

}
