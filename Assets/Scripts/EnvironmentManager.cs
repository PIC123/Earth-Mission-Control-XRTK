using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject water;
    public float waterLevel;
    public Slider waterSlider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waterSlider.value > 0.1)
        {
            water.SetActive(true);
            water.transform.position = new Vector3(water.transform.position.x, waterSlider.value - 1, water.transform.position.z);
        }
        else
        {
            water.SetActive(false);
        }
    }
}
