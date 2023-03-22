using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Microsoft.Geospatial;
using Microsoft.Maps.Unity;

public class GlobeManager : MonoBehaviour
{
    [Serializable]
    public class MarkerList
    {
        public Marker[] markers;
    }

    [Serializable]
    public class Marker
    {
        public float latitude;
        public float longitude;
        public string title;
        public string description;
    }

    public GameObject markerPrefab;
    public string fileName;
    public MapRenderer mapRenderer;
    private MarkerList markerList;
    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        radius = gameObject.transform.localScale.x / 1.75f;
        TextAsset txtAsset = (TextAsset)Resources.Load(fileName);
        markerList = JsonUtility.FromJson<MarkerList>(txtAsset.text);
        //Debug.Log("test");
        //Debug.Log(markers.markers[0].title);
        foreach (Marker marker in markerList.markers)
        {
            //Get correct position
            var correctedPos = ConvertLatLong(marker.latitude, marker.longitude, radius) + transform.position;
            // Get correct orientation
            var correctedRot = AlignRotation(correctedPos);
            // Instantiate marker
            var mapMarker = Instantiate(markerPrefab, correctedPos, correctedRot, transform);
            var mapPinManager = mapMarker.GetComponent<MapPinManager>();
            mapPinManager.markerData = marker;
            Debug.Log(marker.title);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void Read(string path)
    //{
    //    string[] Lines = System.IO.File.ReadAllLines(path);
    //    string[] Columns = Lines[/*   INDEX  */].Split(';');
    //    for (int i = 0; i <= Lines.Length - 1; i++)
    //    {
    //        Debug.Log(Lines[i]);
    //    }
    //}

    public Quaternion AlignRotation(Vector3 markerPos)
    {
        var lookDir = markerPos - transform.position; //Find the correct rotation
        var addition = new Vector3(90, 0, 0);
        //var addition = new Vector3(0, 0, 90);
        return Quaternion.LookRotation(lookDir, Vector3.up) * Quaternion.Euler(addition);
    }

    public Vector3 ConvertLatLong(float latitude, float longitude, float radius)
    {
        latitude = Mathf.PI * latitude / 180;
        longitude = Mathf.PI * longitude / 180;

        // adjust position by radians	
        latitude -= 1.570795765134f; // subtract 90 degrees (in radians)

        // and switch z and y (since z is forward)
        float xPos = (radius) * Mathf.Sin(latitude) * Mathf.Cos(longitude);
        float zPos = (radius) * Mathf.Sin(latitude) * Mathf.Sin(longitude);
        float yPos = (radius) * Mathf.Cos(latitude);


        // return new position
        return new Vector3(xPos, yPos, zPos);
    }
}