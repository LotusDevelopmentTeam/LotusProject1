using Assets.Scripts.SceneManagers;
using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
    // NetworkManager calls it when Server has packet for this Manager
    public abstract void Response(Packet packet);


    //Send packet to server
    public void Send(Packet packet)
    {
        NetworkManager.SendPacket(packet);
    }
}
