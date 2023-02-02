using UnityEngine;
using System;

public class VineCreator : MonoBehaviour
{
    public event EventHandler DamageFor;
    private enum MouseButton
    {
        LeftClick = 0,
        RightClick = 1
    }

    private const int MouseButtonIndex = (int)MouseButton.LeftClick;
    private const int MouseButtonRight = (int)MouseButton.RightClick;

    [SerializeField]
    private LineRenderer tempLineRenderer;
    [SerializeField]
    private float distance;

    // TODO: snap origin to a nde, only allow dragging from valid nodes

    private Node closestNodeToClickPos = null;

    private bool isMouseDown = false;

    private void Update()
    {
        HandleInput();

        if (isMouseDown)
        {
            // Draw a temporary line to the mouse
            // TODO: add a new material to show the line is temporary
            tempLineRenderer.SetPosition(tempLineRenderer.positionCount - 1, GetMousePositionInWorldSpace());
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(MouseButtonIndex))
        {
            // Find the node nearest the mouse click
            Vector3 mouseWorldPosition = GetMousePositionInWorldSpace();
            closestNodeToClickPos = FindClosestNode(mouseWorldPosition);
            float dist = Vector3.Distance(mouseWorldPosition, closestNodeToClickPos.transform.position);
            if (dist < distance)
            {
                // Show a temporary line to the mouse position
                tempLineRenderer.positionCount = 2;
                tempLineRenderer.SetPosition(0, closestNodeToClickPos.transform.position);

                isMouseDown = true;
            }


        }

        if (Input.GetMouseButtonUp(MouseButtonIndex))
        {
            if (isMouseDown)
            {
                // Link two nodes
                Vector3 releasePosition = GetMousePositionInWorldSpace();
                Node closestNodeToReleasePos = FindClosestNode(releasePosition);
                CreateEdge(closestNodeToClickPos, closestNodeToReleasePos);

                // Hide the temporary line, final rendering is handled by the node class
                tempLineRenderer.positionCount = 0;

                closestNodeToClickPos = null;
                isMouseDown = false;
            }

        }
        //TODO
        //CAN BE MOVED LATER IF NEEDED, WAS JUST TESTING.
        if (Input.GetMouseButtonDown(MouseButtonRight))
        {
            Vector3 mouseWorldPosition = GetMousePositionInWorldSpace();
            Node target = FindClosestNode(mouseWorldPosition);
            float dist = Vector3.Distance(mouseWorldPosition, target.transform.position);
            if (dist < distance)
            {
                target.healthManager.HealFor(20);
            }
        }
        //
    }

    private void CreateEdge(Node n1, Node n2)
    {
        // The graph is bidirectional
        n1.AddAdjacentNode(n2);
        n2.AddAdjacentNode(n1);
    }

    private Node FindClosestNode(Vector3 point)
    {
        Debug.Assert(GameManager.Instance.Nodes.Count > 0);
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

    // TODO: This could be moved to a set of utility scripts so other classes can use it too
    private Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosWorld.z = 0;
        return mousePosWorld;
    }
}
