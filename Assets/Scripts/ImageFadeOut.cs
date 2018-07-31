using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageFadeOut : MonoBehaviour {

    public Image FadeImage;
    public Image FadeBG;
    public int loadLevel;
    public float TimeToFadeIn;
    public float TimeToFadeOut;
    public float TimeToStayOnScreen;
    Component[] Texts;

    IEnumerator Start() {
        if (this.GetComponentsInChildren<Text>() != null)
        {
            Texts = this.GetComponentsInChildren<Text>();
            foreach (Text text in Texts)
                text.CrossFadeAlpha(0.0f, -1.0f, false);
        }
        FadeImage.canvasRenderer.SetAlpha(0.0f);



        FadeIn();
        PlayVoiceOver();
        yield return new WaitForSeconds(TimeToStayOnScreen);
        FadeOut();
        yield return new WaitForSeconds(TimeToFadeOut);
        Destroy(FadeBG.gameObject);
      //  SceneManager.LoadScene(loadLevel);
        
    }
	
	// Update is called once per frame
	void FadeIn () {
        FadeImage.CrossFadeAlpha(1.0f, 1.5f, false);
        foreach (Text text in Texts)
            text.CrossFadeAlpha(1.0f, TimeToFadeIn, false);
	}

    void FadeOut()
    {
        FadeImage.CrossFadeAlpha(0.0f, 2.5f, false);
        FadeBG.CrossFadeAlpha(0.0f, 2.5f, false);
        foreach (Text text in Texts)
            text.CrossFadeAlpha(0.0f, TimeToFadeOut, false);
    }

    void PlayVoiceOver() {
        FindObjectOfType<VoiceOverManager>().PlayFirstAudioClip();
        FindObjectOfType<VoiceOverManager>().PlayThirdAudioClip();
    }
}
