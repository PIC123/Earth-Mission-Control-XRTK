using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Geospatial;
using Microsoft.Maps.Unity;

public class MapPinManager : MonoBehaviour
{
    public GlobeManager.Marker markerData;
    private MapRenderer mapRenderer;
    private SpinFree spinner;

    // Start is called before the first frame update
    void Start()
    {
        mapRenderer = gameObject.GetComponentInParent<GlobeManager>().mapRenderer;
        spinner = gameObject.GetComponentInParent<SpinFree>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLatLong()
    {
        mapRenderer.Center = new LatLon(markerData.latitude, markerData.longitude);
    }

    public void toggleSpin(bool spinning)
    {
        spinner.spin = spinning;
    }
}
