using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class SyncedCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    // Use this for initialization
    void Start()
    {
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        this.Enabled = false;
    }

    public bool Enabled { get; private set; }

    public void UpdateCursor(Vector3 point, Quaternion rotation, Vector3 scale)
    {
        this.meshRenderer.enabled = true;
        this.Enabled = true;
        this.transform.position = point;
        this.transform.rotation = rotation;
        this.transform.localScale = scale;
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
                values[9]
            );
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