using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Unity;
using UnityEngine;


// SendMessage is 400 times slower than directly call.
public class ServerMessageHandler
{
    public MessageManager.HoloMessageType MsgType { get; private set; }
    public MessageCallback Handler { get; private set; }

    public delegate void MessageCallback(long userId, string msg);


    public ServerMessageHandler(MessageManager.HoloMessageType msgType, MessageCallback handler)
    {
        this.MsgType = msgType;
        this.Handler = handler;
    }
}

public class MessageManager : Singleton<MessageManager>
{
    public enum HoloMessageType : byte
    {
        DebugMsg = HoloToolkit.Sharing.MessageID.UserMessageIDStart,
        ChangeSize,
        Max
    }


    private NetworkConnection _serverConnection;
    private NetworkConnectionAdapter _connectionAdapter;
    public long LocalUserId { get; set; }
    private SlidersCommands _sliderCommand;


    public delegate void MessageCallback(NetworkInMessage msg);

    private Dictionary<HoloMessageType, ServerMessageHandler.MessageCallback> _messageHandlers =
        new Dictionary<HoloMessageType, ServerMessageHandler.MessageCallback>();


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

        _sliderCommand = GetComponentInParent<SlidersCommands>();


        _messageHandlers = new Dictionary<HoloMessageType, ServerMessageHandler.MessageCallback>()
        {
            {HoloMessageType.DebugMsg, _sliderCommand.ShowServerMsg}
        };


        foreach (var entry in _messageHandlers)
        {
            _serverConnection.AddListener((byte) entry.Key, _connectionAdapter);
        }

//        MessageHandlers.Add();
//        MessageHandlers.Add(HoloMessageType.DebugMsg, LogDebugMsg);
//        MessageHandlers.Add(HoloMessageType.ChangeSize, HandleChangeSize);

//        _serverConnection.AddListener((byte) HoloMessageType.DebugMsg, _connectionAdapter);

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
        var messageType = msg.ReadByte();
        var userId = msg.ReadInt64();
        string messageContent = msg.ReadString();

        var functionToCall = _messageHandlers[(HoloMessageType) messageType];
        if (functionToCall != null)
        {
            functionToCall(userId, messageContent);
        }
    }

    #region Handlers

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

    #endregion
}