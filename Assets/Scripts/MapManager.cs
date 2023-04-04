using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Geospatial;
using Microsoft.Maps.Unity;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public MapRenderer mapRenderer;
    public JoystickBallController controller;
    public float translationSpeedOffset = 1;
    public float rotationSpeedOffset = 1;
    public float zoomSpeedOffset = 1;
    public Text mapInfoText;
    public GameObject water;
    public float waterLevel;
    public Slider waterSlider;
    public float initialWaterHeight;
    // Start is called before the first frame update
    void Start()
    {
        initialWaterHeight = water.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (waterSlider.value > waterSlider.maxValue * 0.1f)
        {
            water.SetActive(true);
            water.transform.position = new Vector3(water.transform.position.x, initialWaterHeight + waterSlider.value, water.transform.position.z);
        }
        else
        {
            water.SetActive(false);
        }

        if (controller.isGrabbed)
        {
            mapRenderer.Center = new LatLon(
                mapRenderer.Center.LatitudeInDegrees + controller.zdist * translationSpeedOffset,
                mapRenderer.Center.LongitudeInDegrees + controller.xdist * translationSpeedOffset
            );
        }

        mapInfoText.text = $"Latitude: {mapRenderer.Center.LatitudeInDegrees} \n Longitude: {mapRenderer.Center.LongitudeInDegrees} \n Zoom Level: {mapRenderer.ZoomLevel}";
    }
}
