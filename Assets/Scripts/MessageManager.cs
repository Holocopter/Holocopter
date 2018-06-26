using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Unity;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>
{
    private NetworkConnection _serverConnection;
    private NetworkConnectionAdapter _connectionAdapter;
    public long LocalUserId { get; set; }


    public delegate void MessageCallback(NetworkInMessage msg);

    public enum HoloMessageType : byte
    {
        DebugMsg = HoloToolkit.Sharing.MessageID.UserMessageIDStart,
        ChangeSize,
        Max
    }

    private Dictionary<HoloMessageType, MessageCallback> _messageHandlers =
        new Dictionary<HoloMessageType, MessageCallback>();

    public Dictionary<HoloMessageType, MessageCallback> MessageHandlers
    {
        get { return _messageHandlers; }
    }


    // Use this for initialization
    void Start()
    {
        if (SharingStage.Instance.IsConnected)
        {
            Connected();
        }
        else
        {
            SharingStage.Instance.SharingManagerConnected += Connected;
        }
    }

    private void Connected(object sender = null, EventArgs e = null)
    {
        SharingStage.Instance.SharingManagerConnected -= Connected;
        InitMessageHandlers();
    }

    private void InitMessageHandlers()
    {
        SharingStage sharingStage = SharingStage.Instance;
        if (sharingStage == null)
        {
            Debug.Log("Cannot Initialize CustomMessages. No SharingStage instance found.");
            return;
        }

        _serverConnection = sharingStage.Manager.GetServerConnection();
        if (_serverConnection == null)
        {
            Debug.Log("Cannot initialize CustomMessages. Cannot get a server connection.");
            return;
        }

        _connectionAdapter = new NetworkConnectionAdapter();
        _connectionAdapter.MessageReceivedCallback += OnMessageReceived;

        LocalUserId = SharingStage.Instance.Manager.GetLocalUser().GetID();


//        MessageHandlers.Add();
//        MessageHandlers.Add(HoloMessageType.DebugMsg, LogDebugMsg);
//        MessageHandlers.Add(HoloMessageType.ChangeSize, HandleChangeSize);

        _serverConnection.AddListener((byte) HoloMessageType.DebugMsg, _connectionAdapter);

        InvokeRepeating("SendDebugMessage", 1.0f, 5.0f);
    }

    private NetworkOutMessage CreateMessage(byte messageType)
    {
        NetworkOutMessage msg = _serverConnection.CreateMessage(messageType);
        msg.Write(messageType);
        msg.Write(LocalUserId);
        return msg;
    }

    private void OnMessageReceived(NetworkConnection connection, NetworkInMessage msg)
    {
        Debug.Log("Messesage Received...");
        byte messageType = msg.ReadByte();

        MessageCallback functionToCall = MessageHandlers[(HoloMessageType) messageType];
        if (functionToCall != null)
        {
            functionToCall(msg);
        }
    }


    private void LogDebugMsg(NetworkInMessage msg)
    {
        var userId = msg.ReadInt64();

        var a = msg.ReadString();
        Debug.Log(string.Format("{0} Debug message from {1}: {2}", LocalUserId, userId, a));
    }

    private void HandleChangeSize(NetworkInMessage msg)
    {
        var userId = msg.ReadInt64();
        var size = msg.ReadInt32();
    }

    public void SendSizeInfo()
    {
        var msg = CreateMessage((byte) HoloMessageType.ChangeSize);
        msg.Write(10);
        _serverConnection.Broadcast(msg);
    }

    public void SendDebugMessage()
    {
        Debug.Log("Send debug message to server...");

        string debugMsg = string.Format("{0} is alive!", LocalUserId);
        NetworkOutMessage msg = CreateMessage((byte) HoloMessageType.DebugMsg);
        msg.Write(debugMsg);
        _serverConnection.Broadcast(msg);
    }
}