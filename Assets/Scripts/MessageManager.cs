using HoloToolkit.Sharing;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// SendMessage is 400 times slower than directly call.
public class MessageManager : Singleton<MessageManager>
{
    public enum HoloMessageType : byte
    {
        DebugMsg = HoloToolkit.Sharing.MessageID.UserMessageIDStart,
        ChangeSlider,
        ChangeModel,
        ChangeCursor,
        Max
    }

    private int _frameCountSinceLastSync = 0;
    private const int FrameInterval = 1;

    private NetworkConnection _serverConnection;
    private NetworkConnectionAdapter _connectionAdapter;

    public long LocalUserId { get; set; }
    private SlidersCommands _sliderCommand;
    private TwoHandManipulatable _handManipulatable;
    private SyncedCursor _syncedCursor;

    public bool IsMaster
    {
        get { return SharingStage.Instance.SessionUsersTracker.CurrentUsers[0].GetID() == LocalUserId; }
    }

    public delegate void MessageCallback(long userId, string msgKey, List<float> values);

    private Dictionary<HoloMessageType, MessageCallback> _messageHandlers =
        new Dictionary<HoloMessageType, MessageCallback>();

    private const bool Debug = true;


    // Use this for initialization
    void Start()
    {
        _sliderCommand = GetComponentInParent<SlidersCommands>();
        _handManipulatable = GameObject.Find("Model").GetComponent<TwoHandManipulatable>();
        _messageHandlers = new Dictionary<HoloMessageType, MessageCallback>()
        {
            {HoloMessageType.DebugMsg, _sliderCommand.ShowServerMsg},
            {HoloMessageType.ChangeSlider, _sliderCommand.NetControlOnSlider},
            {HoloMessageType.ChangeModel, _handManipulatable.SyncFromNetwork}
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

    private NetworkOutMessage CreateMessage(byte messageType, string msgKey, IReadOnlyCollection<float> values)
    {
        NetworkOutMessage msg = _serverConnection.CreateMessage(messageType);
        msg.Write(messageType);
        msg.Write(LocalUserId);

        msg.Write(msgKey);
        msg.Write(values.Count);

        foreach (var value in values)
        {
            msg.Write(value);
        }

        return msg;
    }

    private void OnMessageReceived(NetworkConnection connection, NetworkInMessage msg)
    {
        var messageType = msg.ReadByte();
        var userId = msg.ReadInt64();
        string messageKey = msg.ReadString();
        var floatCount = msg.ReadInt32();

        var floats = new List<float>();
        for (var i = 0; i < floatCount; i++)
        {
            floats[i] = msg.ReadFloat();
        }

        var functionToCall = _messageHandlers[(HoloMessageType) messageType];
        functionToCall?.Invoke(userId, messageKey, floats);
    }

    #region SendMessage

    public void SyncMessage(HoloMessageType type, string key, List<float> values)
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


        UnityEngine.Debug.Log($"Sending key {key} value {string.Join(" ", values.Select(x => x.ToString()))}");
        var msg = CreateMessage((byte) type, key, values);
        _serverConnection.Broadcast(msg);
    }


    public void SendDebugMessage()
    {
        UnityEngine.Debug.Log($"Send debug message to server, I'm {IsMaster}...");

        SyncMessage(HoloMessageType.DebugMsg, $"{LocalUserId} is alive", new List<float>() {0});
    }

    #endregion
}