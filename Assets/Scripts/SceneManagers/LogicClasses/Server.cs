using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Assets.Scripts.SceneManagers;
using System.Net;

public static class Server
{
    static TcpClient client = new TcpClient();
    static NetworkStream stream;
    public static string Ip { get; set; }
    public static int Port { get; set; }


    public static void Connect()
    {
        try
        {
            Debug.Log("Connecting...");
            client.Connect(Ip, Port);
            stream = client.GetStream();
        }
        catch (Exception e)
        {
            Debug.Log("Failed To Connect");
            Debug.Log(e.Message);
            //throw;
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
        return new Packet("");
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
            throw;
        }
    }

    public static void Disconnect()
    {
        Packet packet = new Packet("");
        //TODO SEND DISCONNECT PACKET
        stream.Close();
        client.Close();
        client.Dispose();
    }

    public static bool isConnected()
    {
        if (client == null) return false;
        return client.Connected;
    }

}

