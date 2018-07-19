using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HoloToolkit.Unity;
using UnityEngine;

public class SyncedCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    // Use this for initialization
    void Start()
    {
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>(true);
        meshRenderer.enabled = false;
    }

    public bool UseUnscaledTime = true;

    /// <summary>
    /// Blend value for surface normal to user facing lerp
    /// </summary>
    public float PositionLerpTime = 0.01f;

    /// <summary>
    /// Blend value for surface normal to user facing lerp
    /// </summary>
    public float ScaleLerpTime = 0.01f;

    /// <summary>
    /// Blend value for surface normal to user facing lerp
    /// </summary>
    public float RotationLerpTime = 0.01f;

    public void UpdateCursor(Vector3 point, Vector3 scale, Quaternion rot)
    {
        float deltaTime = UseUnscaledTime
            ? Time.unscaledDeltaTime
            : Time.deltaTime;
        this.meshRenderer.enabled = true;

        transform.position = Vector3.Lerp(transform.position, point, deltaTime / PositionLerpTime);
        transform.localScale = Vector3.Lerp(transform.localScale, scale, deltaTime / ScaleLerpTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, deltaTime / RotationLerpTime);
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
            var scale = new Vector3(
                values[3],
                values[4],
                values[5]
            );
            var rot = new Quaternion(
                values[6],
                values[7],
                values[8],
                values[9]);
            UpdateCursor(pos, scale, rot);
        }
    }

    public void SetCursorState(bool state)
    {
        this.meshRenderer.enabled = state;
    }

    // Update is called once per frame
    void Update()
    {
    }
}