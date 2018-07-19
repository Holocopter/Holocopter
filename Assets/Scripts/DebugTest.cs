using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTest : MonoBehaviour
{
    public Button btn;

    // Use this for initialization
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(test);
        Sharing = GameObject.Find("/Sharing");
        Sharing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public GameObject Sharing;

    void test()
    {
        Debug.Log("button triggered");

//        Sharing = GameObject.Find("/Sharing");
        Sharing.SetActive(true);
    }
}