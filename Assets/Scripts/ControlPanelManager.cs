using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelManager : MonoBehaviour
{
    public GameObject[] panels;
    private int activePanel = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchActivePanel(int newActivePanel)
    {
        if(newActivePanel != activePanel)
        {
            panels[activePanel].SetActive(false);
            panels[newActivePanel].SetActive(false);
            activePanel = newActivePanel;
        }
    }
}
