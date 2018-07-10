using HoloToolkit.Unity.UX;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using HoloToolkit.Unity.InputModule;


namespace HoloToolkit.Unity.InputModule.Utilities.Interactions
{ 

public class HandManipulationShader : MonoBehaviour, IInputHandler, ISourceStateHandler
    {
        [SerializeField]
        protected GameObject rotorOrigin;
        [SerializeField]
        protected GameObject rotorFrame;
        

        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		


	}

        public void OnInputDown(InputEventData eventData)
        {
            // Add to hand map\
            if (rotorOrigin != null)
            {
                rotorOrigin.SetActive(false);
            }
            if (rotorFrame != null)
            {
                rotorFrame.SetActive(true);
            }
      

        }

        /// <summary>
        /// Event Handler receives input from inputSource
        /// </summary>
        public void OnInputUp(InputEventData eventData)
        {
            if (rotorOrigin != null)
            {
                rotorOrigin.SetActive(true);
            }
            if (rotorFrame != null)
            {
                rotorFrame.SetActive(false);
            }

        }

        public void OnSourceDetected(SourceStateEventData eventData) { }

        /// <summary>
        /// OnSourceLost
        /// </summary>
        public void OnSourceLost(SourceStateEventData eventData)
        {
            if (rotorOrigin != null)
            {
                rotorOrigin.SetActive(true);
            }
            if (rotorFrame != null)
            {
                rotorFrame.SetActive(false);
            }
        }


    }

}