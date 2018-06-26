using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public struct VoiceCommand
{
    public string Command;
    public VoiceCallBack Handler;
    public string Parameter;

    public delegate void VoiceCallBack(string command);

    public VoiceCommand(string command, VoiceCallBack handler, string parameter)
    {
        this.Command = command;
        this.Handler = handler;
        this.Parameter = parameter;
    }
}

public class SpeechManager : MonoBehaviour
{
    private KeywordRecognizer _keywordRecognizer = null;
    private readonly Dictionary<string, System.Action> _keywords = new Dictionary<string, System.Action>();

    private List<VoiceCommand> _voiceCommands;

    // Use this for initialization
    void Start()
    {
        var sliderCommand = this.GetComponentInParent<SlidersCommands>();

        _voiceCommands = new List<VoiceCommand>
        {
            new VoiceCommand("Make Faster", sliderCommand.VoiceControlOnSlider, "Faster"),
            new VoiceCommand("Make Slower", sliderCommand.VoiceControlOnSlider, "Slower"),
            new VoiceCommand("Make Bigger", sliderCommand.VoiceControlOnSlider, "Bigger"),
            new VoiceCommand("Make Smaller", sliderCommand.VoiceControlOnSlider, "Smaller"),
            new VoiceCommand("Increase Collective", sliderCommand.VoiceControlOnSlider, "Coll_de"),
            new VoiceCommand("Decrease Collective", sliderCommand.VoiceControlOnSlider, "Coll_in"),
            new VoiceCommand("Show Airflow", sliderCommand.VoiceControlOnButton, "WindFX_ON"),
            new VoiceCommand("Hide Airflow", sliderCommand.VoiceControlOnButton, "WindFX_OFF"),
            new VoiceCommand("Show Fixed Camera", sliderCommand.VoiceControlOnSprite, "FixedCam_ON"),
            new VoiceCommand("Hide Fixed Camera", sliderCommand.VoiceControlOnSprite, "FixedCam_OFF"),
            new VoiceCommand("Reset The Scene", sliderCommand.VoiceControlOnScene, "Reset")
        };

        foreach (var voice in _voiceCommands)
        {
            var voice1 = voice;
            _keywords.Add(voice.Command,
                () => { voice1.Handler(voice1.Parameter); });
        }

        // Tell the KeywordRecognizer about our keywords.
        _keywordRecognizer = new KeywordRecognizer(_keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        _keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        _keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (_keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}