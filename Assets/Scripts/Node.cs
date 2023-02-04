using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    // TODO Move these to data source
    private const float LineWidth = 0.1f;
    private Color lineColor = Color.green;
    ////////////

    protected List<Node> adjacentNodes = new List<Node>(); // edges will be stored as an adjacency list of other nodes, we expect a sparse graph by design

    public int Depth { get; private set; }

    public int EdgeCount { get; private set; }

    public HealthSystem HealthSystem { get; private set; }

    void Awake()
    {
        HealthSystem = gameObject.GetComponentInChildren<HealthSystem>();
    }

    public void AddAdjacentNode(Node n)
    {
        adjacentNodes.Add(n);

        // FIXME extremely inefficient to just spawn a bunch of line renderers - this is for prototyping purposes only
        DrawEdge(transform.position, n.transform.position);
    }

    private void DrawEdge(Vector3 origin, Vector3 destination)
    {
        // Only one line renderer can be present on a GameObject at a time
        var container = Instantiate(new GameObject());
        container.transform.parent = transform;

        // Configure the line renderer's settings
        var lr = container.AddComponent<LineRenderer>();
        lr.startWidth = LineWidth;
        lr.endWidth = LineWidth;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lineColor;
        lr.endColor = lineColor;
        lr.positionCount = 2;
        lr.SetPosition(0, origin);
        lr.SetPosition(1, destination);
    }
}
