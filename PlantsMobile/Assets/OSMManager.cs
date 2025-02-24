using OsmSharp.API;
using OsmSharp.Streams;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OSMManager : MonoBehaviour
{
    [SerializeField] private string m_FilePath = "C:\\Users\\Lucy\\Downloads\\map.osm";
    // Start is called before the first frame update
    void Start()
    {
        GPSManager.instance.RefreshGPSCoordSystem();

        Osm mapData = LoadOsmFileXML(m_FilePath);

        // Contructing map from nodes 
        foreach(OsmSharp.Node node in mapData.Nodes)
        {
            if(node.Latitude == null || node.Longitude == null) continue;

            Vector2 gpsLoc = new((float)node.Latitude.Value, (float)node.Longitude.Value);

            GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObject.transform.SetParent(gameObject.transform);
            newObject.name = $"Node - {node.Id.ToString()}";
            newObject.transform.position = GPSEncoder.GPSToUCS(gpsLoc);
        }

        return;

        foreach(OsmSharp.Way currentWay in mapData.Ways)
        {
            bool isCurrentWayGreenSpace = false;

            if (currentWay.Tags == null)
            {
                continue;
            }

            else
            {
                foreach (OsmSharp.Tags.Tag tag in currentWay.Tags)
                {
                    if (tag.Key == "natural")
                    {
                        if (tag.Value == "wood")
                        {
                            isCurrentWayGreenSpace = true;
                        }
                    }
                }
            }

            if(isCurrentWayGreenSpace)
            {
                //greenspace!
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static Osm LoadOsmFileXML(string filePath)
    {
        using (FileStream fileStream = File.OpenRead(filePath))
        {
            var sourceStream = new XmlOsmStreamSource(fileStream);

            Osm osm = new Osm();

            List<OsmSharp.Node> newNodes = new List<OsmSharp.Node>();
            List<OsmSharp.Way> newWays = new List<OsmSharp.Way>();
            List<OsmSharp.Relation> newRelations = new List<OsmSharp.Relation>();

            // Add the elements to the Osm object
            foreach (var osmGeo in sourceStream)
            {
                switch (osmGeo.Type)
                {
                    case OsmSharp.OsmGeoType.Node:
                        newNodes.Add(osmGeo as OsmSharp.Node);
                        break;
                    case OsmSharp.OsmGeoType.Way:
                        newWays.Add(osmGeo as OsmSharp.Way);
                        break;
                    case OsmSharp.OsmGeoType.Relation:
                        newRelations.Add(osmGeo as OsmSharp.Relation);
                        break;
                }
            }

            osm.Nodes = newNodes.ToArray();
            osm.Ways = newWays.ToArray();
            osm.Relations = newRelations.ToArray();

            return osm;
        }
    }
}
