using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public struct VoiceCommand
{
    public string Command;
    public string Handler;
    public string Parameter;

    public VoiceCommand(string command, string handler, string parameter)
    {
        this.Command = command;
        this.Handler = handler;
        this.Parameter = parameter;
    }
}

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private readonly List<VoiceCommand> _voiceCommands = new List<VoiceCommand>
    {
        new VoiceCommand("Make Faster", "VoiceControlOnSlider", "Faster"),
        new VoiceCommand("Make Slower", "VoiceControlOnSlider", "Slower"),
        new VoiceCommand("Make Bigger", "VoiceControlOnSlider", "Bigger"),
        new VoiceCommand("Make Smaller", "VoiceControlOnSlider", "Smaller"),
        new VoiceCommand("Increase Collective", "VoiceControlOnSlider", "Coll_de"),
        new VoiceCommand("Decrease Collective", "VoiceControlOnSlider", "Coll_in"),
        new VoiceCommand("Show Airflow","VoiceControlOnButton","WindFX_ON"),
        new VoiceCommand("Hide Airflow","VoiceControlOnButton","WindFX_OFF"),
        new VoiceCommand("Show Fixed Camera","VoiceControlOnSprite","FixedCam_ON"),
        new VoiceCommand("Hide Fixed Camera","VoiceControlOnSprite","FixedCam_OFF"),
    };

    // Use this for initialization
    void Start()
    {
        var sliderCommand = this.GetComponentInParent<SlidersCommands>();
        foreach (var voice in _voiceCommands)
        {
            var voice1 = voice;
            keywords.Add(voice.Command,
                () => { sliderCommand.SendMessage(voice1.Handler, voice1.Parameter); });
        }

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}