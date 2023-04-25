using Oculus.Voice;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class VoiceIntentController : MonoBehaviour
{
    [Header("Voice")]
    [SerializeField]
    private AppVoiceExperience appVoiceExperience;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI fullTranscriptText;

    [SerializeField]
    private TextMeshProUGUI partialTranscriptText;

    private bool appVoiceActive;

    private void Awake()
    {
        fullTranscriptText.text = partialTranscriptText.text = string.Empty;

        appVoiceExperience.TranscriptionEvents.OnFullTranscription.AddListener((transcript) =>
        {
            fullTranscriptText.text = transcript;
        });

        appVoiceExperience.TranscriptionEvents.OnPartialTranscription.AddListener((transcript) =>
        {
            partialTranscriptText.text = transcript;
        });

        appVoiceExperience.VoiceEvents.OnRequestCreated.AddListener((request) =>
        {
            appVoiceActive = true;
            Debug.Log("OnRequestCreated Activated");
        });

        appVoiceExperience.VoiceEvents.OnRequestCompleted.AddListener(() =>
        {
            appVoiceActive = false;
            Debug.Log("OnRequestCompleted Deactivated");
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !appVoiceActive)
        //{
        //    appVoiceExperience.Activate();
        //}
        //if (Input.GetButton("trigger"))
        {
            Debug.Log("trigger pressed");
        }
    }

    private static void DisplayValues(string prefix, string[] info)
    {
        foreach (var i in info)
        {
            Debug.Log($"{prefix} {i}");
        }
    }

    public void MoveToDisplay(String[] info)
    {
        DisplayValues("MoveToDisplay: ", info);
    }

    public void LoadLocation(String[] info)
    {
        DisplayValues("LoadLocation: ", info);
    }

    public void LoadData(String[] info)
    {
        DisplayValues("LoadData: ", info);
    }

    public void ChangeZoom(String[] info)
    {
        DisplayValues("ChangeZoom: ", info);
    }

    public void Help(String[] info)
    {
        DisplayValues("Help: ", info);
    }
}
