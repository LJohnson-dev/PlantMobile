using OsmSharp.API;
using OsmSharp.Streams;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OSMManager : MonoBehaviour
{
    [SerializeField] private DebugOSMNode m_DebugNodePrefab = null;
    [SerializeField] private string m_ResourcesRelativeFilePath;

    private Dictionary<string, List<string>> m_GreenSpaceTagIdentifiers = new Dictionary<string, List<string>>()
    {
        {"natural", new List<string>(){"wood", "farm" } },
        {"landuse", new List<string>(){"grass" } },
        {"leaf_type", new List<string>(){"*" } },
    };

    // Generated from BuildMapData
    private Dictionary<long, OsmSharp.Node> m_NodeLUT = new Dictionary<long, OsmSharp.Node>();
    private List<OsmSharp.Node> m_GreenspaceNodes = new List<OsmSharp.Node>();
    private HashSet<long> m_GreenspaceNodeIDs = new HashSet<long>();

    // Debug
    private DebugOSMNode m_Player = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = Instantiate(m_DebugNodePrefab, gameObject.transform);
        m_Player.name = "DebugPlayer";
        m_Player.SetColour(Color.yellow);

        GPSManager.instance.RefreshGPSCoordSystem();

        Osm mapData = LoadOsmFileXML(m_ResourcesRelativeFilePath);

        BuildMapData(mapData);
    }

    private void BuildMapData(Osm mapData)
    {
        m_NodeLUT.Clear();
        m_GreenspaceNodes.Clear();

        // Create lookup for ID -> OsmSharp.Node
        foreach (OsmSharp.Node node in mapData.Nodes)
        {
            m_NodeLUT.Add(node.Id.Value, node);
            if(HasGreenspaceTag(node.Tags))
            {
                m_GreenspaceNodes.Add(node);
                m_GreenspaceNodeIDs.Add(node.Id.Value);
            }
        }

        foreach (OsmSharp.Way currentWay in mapData.Ways)
        {
            if (currentWay.Tags == null)
            {
                continue;
            }

            if (HasGreenspaceTag(currentWay.Tags))
            {
                for (int i = 0; i < currentWay.Nodes.Length; ++i)
                {
                    long nodeID = currentWay.Nodes[i];
                    OsmSharp.Node currentNode = m_NodeLUT[nodeID];

                    m_GreenspaceNodes.Add(currentNode);
                    m_GreenspaceNodeIDs.Add(nodeID);
                }
            }
        }

        // Debug
        foreach (OsmSharp.Node node in mapData.Nodes)
        {
            if (node.Latitude == null || node.Longitude == null) continue;

            Vector2 gpsLoc = new((float)node.Latitude.Value, (float)node.Longitude.Value);

            bool isGreenspace = m_GreenspaceNodeIDs.Contains(node.Id.Value);

            DebugOSMNode newNode = Instantiate(m_DebugNodePrefab, gameObject.transform);
            newNode.name = $"Node - {node.Id.ToString()}";
            newNode.transform.position = GPSEncoder.GPSToUCS(gpsLoc);
            newNode.SetColour(isGreenspace ? Color.green : Color.gray);
        }
    }

    private void Update()
    {
        Vector3 newPos = m_Player.transform.position;
        newPos.y = 10;
        m_Player.transform.position = newPos;
        GPSManager.instance.DebugGPSLocation = GPSEncoder.USCToGPS(m_Player.transform.position);
    }

    private bool HasGreenspaceTag(OsmSharp.Tags.TagsCollectionBase tagCollection)
    {
        if(tagCollection == null) return false;

        foreach (OsmSharp.Tags.Tag tag in tagCollection)
        {
            if (m_GreenSpaceTagIdentifiers.ContainsKey(tag.Key))
            {
                if (m_GreenSpaceTagIdentifiers[tag.Key].Contains("*"))
                {
                    return true;
                }

                List<string> allowedValues = m_GreenSpaceTagIdentifiers[tag.Key];
                foreach (string allowedValue in allowedValues)
                {
                    if (tag.Value == allowedValue)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public List<OsmSharp.Node> GetGreenspaceNodes()
    {
        return m_GreenspaceNodes;
    }

    public static Osm LoadOsmFileXML(string filePath)
    {
        TextAsset file = (TextAsset)Resources.Load(filePath, typeof(TextAsset));
        if(file == null)
        {
            Debug.LogError($"Couldn't load file from Resources {filePath}");
            return null;
        }

        using (var stream = new MemoryStream())
        {
            var writer = new StreamWriter(stream);
            writer.Write(file.text);
            writer.Flush();
            stream.Position = 0;

            var sourceStream = new XmlOsmStreamSource(stream);

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
