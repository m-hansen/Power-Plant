using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// A collection of misc helper functions that may be moved to their own classes at some point
public static class Util
{
    public static Node FindClosestNode(Vector3 point)
    {
        Debug.Assert(GameManager.Instance.Nodes.Length > 0);
        float shortestDistance = float.MaxValue;
        Node closestNode = null;
        foreach (Node node in GameManager.Instance.Nodes)
        {
            float distance = Vector3.Distance(point, node.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public static Node[] FindAllClosestNodes(Vector3 point)
    {
        Dictionary<Node, float> nodeDistances = new Dictionary<Node, float>();
        Node[] nodes = GameManager.Instance.Nodes;
        foreach (Node n in nodes)
        {
            float distSq = (n.transform.position - point).sqrMagnitude;
            nodeDistances.Add(n, distSq);
        }

        var sortedNodes = nodeDistances.OrderBy(d => d.Value).ToDictionary(d => d.Key, d => d.Value); // TODO skip converting back to a dictionary for performance
        return sortedNodes.Keys.ToArray();
    }

    public static Settlement[] FindAllClosestSettlements(Vector3 point)
    {
        // FIXME pretty innefficient, for prototyping only

        var allNodes = FindAllClosestNodes(point);

        List<Settlement> allSettlements = new List<Settlement>();
        foreach (Node n in allNodes)
        {
            var settlement = n.gameObject?.GetComponent<Settlement>();
            if (settlement != null)
            {
                allSettlements.Add(settlement);
            }
        }

        return allSettlements.ToArray();
    }

    public static Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosWorld.z = 0;
        return mousePosWorld;
    }
}
