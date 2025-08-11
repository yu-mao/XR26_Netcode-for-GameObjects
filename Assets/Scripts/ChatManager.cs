using System;
using UnityEngine;
using Unity.Netcode;

public class ChatManager : NetworkBehaviour
{
    public void SendChatMessage(string message)
    {
        SendMessageServerRpc(message, NetworkManager.LocalClientId);
    }
    
    [Rpc(SendTo.Server)]
    public void SendMessageServerRpc(string message,
        ulong senderId)
    {
        // Server receives and broadcasts to all
        ReceiveMessageClientRpc(message, senderId);
    }
    
    [Rpc(SendTo.AllClients)]
    public void ReceiveMessageClientRpc(string message,
        ulong senderId)
    {
        // All clients display the message
        string playerName = GetPlayerName(senderId);
        DisplayMessage($"{playerName}: {message}");
    }
}