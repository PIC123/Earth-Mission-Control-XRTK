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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
