using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class SyncedCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private MessageManager syncManager;

    // Use this for initialization
    void Start()
    {
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        syncManager = GameObject.Find("Manager").GetComponent<MessageManager>();
        this.Enabled = false;
        this.enabled = !syncManager.IsMaster;
    }

    public bool Enabled { get; private set; }

    public void UpdateCursor(Vector3 point, Quaternion rot, Vector3 scale)
    {
//        this.meshRenderer.enabled = true;
        this.Enabled = true;
        this.transform.position = point;
        this.transform.localScale = scale;
        this.transform.rotation = rot;
    }

    public void SyncFromNetwork(long userId, string msgKey, List<float> values)
    {
        Debug.Log(
            $"Got change cursor key {msgKey} msg: {string.Join(" ", values.Select(x => x.ToString(CultureInfo.InvariantCulture)))}");
        if (msgKey == "state")
        {
            SetCursorState(values[0] > 0.5);
        }
        else if (msgKey == "cursor")
        {
            var pos = new Vector3(
                values[0],
                values[1],
                values[2]
            );
            var rot = new Quaternion(
                values[3],
                values[4],
                values[5],
                values[6]
            );
            var scale = new Vector3(
                values[7],
                values[8],
                values[9]);
            UpdateCursor(pos, rot, scale);
        }
    }

    public void SetCursorState(bool state)
    {
        this.Enabled = state;
        this.meshRenderer.enabled = state;
    }

    // Update is called once per frame
    void Update()
    {
    }
}