using Oculus.Voice;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Displays
{
    Table = 0,
    Globe = 1,
    Hyperwall = 2
}

public class VoiceIntentController : MonoBehaviour
{
    public GameObject[] DisplayTeleportSpots;

    public GameObject player;

    public MapManager mapManager;

    [Header("Voice")]
    [SerializeField]
    private AppVoiceExperience appVoiceExperience;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI fullTranscriptText;

    [SerializeField]
    private TextMeshProUGUI partialTranscriptText;

    [SerializeField]
    private TextMeshProUGUI responseText;

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
        //if (Keyboard.current.spaceKey.wasPressedThisFrame && !appVoiceActive)
        //{
        //    appVoiceExperience.Activate();
        //}

        //if (Input.GetButton("trigger"))
        //{
        //    Debug.Log("trigger pressed");
        //}
    }

    public void ActivateVoice()
    {
        appVoiceExperience.Activate();
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
        responseText.text = $"MoveToDisplay: {info[0]}";
        if (Enum.TryParse(info[0], true, out Displays disp))
        {
            int dispNum = (int)disp;
            GameObject desiredSpot = DisplayTeleportSpots[dispNum];
            player.transform.position = new Vector3(desiredSpot.transform.position.x, player.transform.position.y, desiredSpot.transform.position.z);
        }
    }

    public void LoadLocation(String[] info)
    {
        DisplayValues("LoadLocation: ", info);
        responseText.text = $"LoadLocation: {info[0]}";
        if (info.Length > 0 && float.TryParse(info[0], out float targetLat) && float.TryParse(info[1], out float targetLong))
        {
            mapManager.setLatLong(targetLat, targetLong);
        }
    }

    public void LoadData(String[] info)
    {
        DisplayValues("LoadData: ", info);
        responseText.text = $"LoadData: {info[0]}";
    }

    public void ChangeZoom(String[] info)
    {
        DisplayValues("ChangeZoom: ", info);
        responseText.text = $"ChangeZoom: {info[0]}";
        if(info.Length > 0 && float.TryParse(info[0], out float targetZoom))
        {
            mapManager.setZoom(targetZoom);
        }
    }

    public void Help(String[] info)
    {
        DisplayValues("Help: ", info);
        responseText.text = $"Help: {info[0]}";
    }
}