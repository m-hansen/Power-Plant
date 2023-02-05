using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(HealthSystem))]
public class Node : MonoBehaviour
{
    private const float LineWidth = 0.467f;
    private const int InvalidDepth = -1;

    protected List<Node> adjacentNodes = new List<Node>(); // edges will be stored as an adjacency list of other nodes, we expect a sparse graph by design

    protected Color edgeColor = Color.black;

    public int Depth { get; private set; } = InvalidDepth;

    public int EdgeCount { get => adjacentNodes.Count; }

    public HealthSystem HealthSystem { get; protected set; } // TODO: 8305

    void Awake()
    {
        // TODO: 8305
        //HealthSystem = gameObject.GetComponentInChildren<HealthSystem>();
    }

    public void AddAdjacentNode(Node n)
    {
        adjacentNodes.Add(n);

        // FIXME extremely inefficient to just spawn a bunch of line renderers - this is for prototyping purposes only
        // let's at least not double draw lines, let the lower depth be the one to actually handle the draw
        if (Depth < n.Depth)
        {
            DrawEdge(transform.position, n.transform.position);
        }

        if (Depth <= InvalidDepth)
        {
            if (this is PowerPlant)
            {
                Depth = 0;
            }
            else if (n is PowerPlant)
            {
                Depth = 1;
            }
            else
            {
               Depth = n.Depth + 1;
            }               
        }
    }

    private void DrawEdge(Vector3 origin, Vector3 destination)
    {
        // Only one line renderer can be present on a GameObject at a time
        var container = Instantiate(new GameObject());
        container.transform.parent = transform;

        // Configure the line renderer's settings
        var lr = container.AddComponent<LineRenderer>();
        var lrRend = lr.GetComponent<Renderer>();
        lr.textureMode = LineTextureMode.Tile;
        lrRend.material = Resources.Load<Material>("Vine");
        float distance = (destination - origin).magnitude;
        lrRend.material.mainTextureScale = new Vector2(distance / 10, lrRend.material.mainTextureScale.y);
        lr.startWidth = LineWidth;
        lr.endWidth = LineWidth;
        lr.startColor = edgeColor;
        lr.endColor = edgeColor;
        lr.positionCount = 2;
        lr.SetPosition(0, origin);
        lr.SetPosition(1, destination);
    }
}
