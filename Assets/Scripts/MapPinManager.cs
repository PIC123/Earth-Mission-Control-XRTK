using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Geospatial;
using Microsoft.Maps.Unity;
using UnityEngine.UI;

public class MapPinManager : MonoBehaviour
{
    public GlobeManager.Marker markerData;
    public MapRenderer mapRenderer;
    public Text overviewText;
    private SpinFree spinner;
    public Material goodMat;
    public Material badMat;



    // Start is called before the first frame update
    void Start()
    {
        mapRenderer = GameObject.FindGameObjectsWithTag("MapTable")[0].GetComponent<MapRenderer>();
        //spinner = gameObject.GetComponentInParent<SpinFree>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setupPin(GlobeManager.Marker marker)
    {
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        if (marker.co2 > 31000f)
        {
            GetComponent<Renderer>().material = badMat;
        }
    }

    public void setLatLong()
    {
        mapRenderer.Center = new LatLon(markerData.latitude, markerData.longitude);
    }

    public void setMapInfo()
    {
        overviewText.text = $"Location: {markerData.title} \n Latitude: {markerData.latitude} \n Longitude: {markerData.longitude} \n Description: {markerData.description}";
    }

    public void toggleSpin(bool spinning)
    {
        spinner.spin = spinning;
    }
}
