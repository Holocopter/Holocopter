using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Unity;
using UnityEngine;


// SendMessage is 400 times slower than directly call.
public class MessageManager : Singleton<MessageManager>
{
    public enum HoloMessageType : byte
    {
        DebugMsg = HoloToolkit.Sharing.MessageID.UserMessageIDStart,
        ChangeSlider,
        Max
    }

    private int _frameCountSinceLastSync = 0;
    private const int FrameInterval = 1;

    private NetworkConnection _serverConnection;
    private NetworkConnectionAdapter _connectionAdapter;

    public long LocalUserId { get; set; }
    private SlidersCommands _sliderCommand;

    public bool IsMaster
    {
        get { return SharingStage.Instance.SessionUsersTracker.CurrentUsers[0].GetID() == LocalUserId; }
    }

    public delegate void MessageCallback(long userId, string msgKey, string msgValue);

    private Dictionary<HoloMessageType, MessageCallback> _messageHandlers =
        new Dictionary<HoloMessageType, MessageCallback>();

    private const bool Debug = true;

    // Use this for initialization
    void Start()
    {
        _sliderCommand = GetComponentInParent<SlidersCommands>();
        _messageHandlers = new Dictionary<HoloMessageType, MessageCallback>()
        {
            {HoloMessageType.DebugMsg, _sliderCommand.ShowServerMsg},
            {HoloMessageType.ChangeSlider, _sliderCommand.NetControlOnSlider}
        };
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
            UnityEngine.Debug.Log("Cannot Initialize CustomMessages. No SharingStage instance found.");
            return;
        }

        _serverConnection = sharingStage.Manager.GetServerConnection();
        if (_serverConnection == null)
        {
            UnityEngine.Debug.Log("Cannot initialize CustomMessages. Cannot get a server connection.");
            return;
        }

        _connectionAdapter = new NetworkConnectionAdapter();
        _connectionAdapter.MessageReceivedCallback += OnMessageReceived;

        LocalUserId = SharingStage.Instance.Manager.GetLocalUser().GetID();

        foreach (var entry in _messageHandlers)
        {
            _serverConnection.AddListener((byte) entry.Key, _connectionAdapter);
        }

        if (Debug)
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
        var messageType = msg.ReadByte();
        var userId = msg.ReadInt64();
        string messageKey = msg.ReadString();
        string messageValue = msg.ReadString();

        var functionToCall = _messageHandlers[(HoloMessageType) messageType];
        if (functionToCall != null)
        {
            functionToCall(userId, messageKey, messageValue);
        }
    }

    #region SendMessage

    public void SyncValue(string key, string value)
    {
        if (!this.IsMaster)
        {
            return;
        }

        if (Time.frameCount - _frameCountSinceLastSync < FrameInterval)
        {
            return;
        }

        _frameCountSinceLastSync = Time.frameCount;
        SendSliderValue(key, value);
    }

    public void SendSliderValue(string whichSlider, string value)
    {
        UnityEngine.Debug.Log(string.Format("Sending slider {0} value {1}", whichSlider, value));
        var msg = CreateMessage((byte) HoloMessageType.ChangeSlider);
        msg.Write(whichSlider);
        msg.Write(value);
        _serverConnection.Broadcast(msg);
    }

    public void SendDebugMessage()
    {
        UnityEngine.Debug.Log(string.Format("Send debug message to server, I'm {0}...", IsMaster));

        string debugMsg = string.Format("{0} is alive!", LocalUserId);
        NetworkOutMessage msg = CreateMessage((byte) HoloMessageType.DebugMsg);
        msg.Write(debugMsg);
        msg.Write("...");
        _serverConnection.Broadcast(msg);
    }

    #endregion
}