using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Scripts.SceneManagers;



public static class Server
{
    public static string Ip { get; set; }
    public static int Port { get; set; }
    private static TcpClient client { get; set; }
    private static NetworkStream stream { get; set; }


    public static void Connect(string ip, int port)
    {
        Ip = ip;
        Port = port;
        try
        {
            client.Connect(Ip, Port);
            stream = client.GetStream();
        }
        catch
        {
            Debug.Log("Trying To Connect");
        }
    }

    public static Packet GetPacket()
    {
        if (stream.CanRead)
        {
            int tmp;
            List<char> tmp_msg = new List<char>();
            try
            {
                while (true)
                {
                    // ReadByte() returns -1 when all bytes were read
                    tmp = stream.ReadByte();
                    if (tmp == -1) break;
                    tmp_msg.Add((char)tmp);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            string msg = new string(tmp_msg.ToArray());
            Packet packet = new Packet(msg);
            return packet;
        }
        Debug.Log("Stream.CanRead = FALSE");
        return new Packet();
    }

    public static void SendPacket(Packet packet)
    {
        try
        {
            byte[] msg_bytes = Encoding.ASCII.GetBytes(packet.Msg);

            stream.Write(msg_bytes, 0, msg_bytes.Length);
            stream.Flush();
        }
        catch
        {
            Debug.Log("Can't Send: " + packet.Msg);
        }
    }

    public static void Disconnect()
    {
        Packet packet = new Packet();
        //TODO SEND DISCONNECT PACKET 
        client.Close();
        client.Dispose(); ;
    }

}

