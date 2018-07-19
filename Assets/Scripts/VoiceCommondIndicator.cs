using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCommondIndicator : MonoBehaviour {

   // public int tester;
   // public int tester_old;
    [SerializeField]
    private int currentLevel;
    VoiceCommand voiceCommand;

    [SerializeField]
    List< GameObject> Cube_indicator = new List<GameObject>();

    public int CurrentLevel
    {
        get { return currentLevel; }
        set
        {
             currentLevel = value;
        }
    }



    public void showIndicator(int level)
    {

        if (Cube_indicator != null && level == 0)

        {
            foreach (GameObject indicator in Cube_indicator)
            {

                indicator.SetActive(false);
            }

        }




        if (Cube_indicator != null && level >= 1)
        {
            for (int i = 1; i <= level; i++)
            {

                Cube_indicator[i - 1].SetActive(true);


            }
            for (int i = Cube_indicator.Count; i > level; i--)
            {

                Cube_indicator[i - 1].SetActive(false);


            }

         

        }else
        {
            return;
        }
      
      

    }

    // Use this for initialization
    void Start () {

        if (Cube_indicator != null)
        {
            foreach (GameObject indicator in Cube_indicator)
            {

                indicator.SetActive(false);
            }

        }
    }
	

	// Update is called once per frame
	void Update () {

            showIndicator(currentLevel);

	}
}
