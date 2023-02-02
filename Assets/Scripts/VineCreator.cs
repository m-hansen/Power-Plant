using UnityEngine;
using System;

public class VineCreator : MonoBehaviour
{
    public event EventHandler DamageFor; // is this in use?

    private enum MouseButton
    {
        LeftClick = 0,
        RightClick = 1
    }

    private const int MouseButtonIndex = (int)MouseButton.LeftClick; // for easily swapping vine drawing keybind

    [SerializeField]
    private LineRenderer tempLineRenderer;
    [SerializeField]
    private float distance;
    [SerializeField]
    private GameObject budPrefab;

    // TODO: only allow dragging from valid nodes

    private Node closestNodeToClickPos = null;

    private bool isMouseDown = false;

    private void Update()
    {
        HandleInput();

        if (isMouseDown)
        {
            // Draw a temporary line to the mouse
            tempLineRenderer.SetPosition(tempLineRenderer.positionCount - 1, Util.GetMousePositionInWorldSpace());
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(MouseButtonIndex))
        {
            // Find the node nearest the mouse click
            Vector3 mouseWorldPosition = Util.GetMousePositionInWorldSpace();
            closestNodeToClickPos = Util.FindClosestNode(mouseWorldPosition);
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
                Vector3 releasePosition = Util.GetMousePositionInWorldSpace();
                Node closestNodeToReleasePos = Util.FindClosestNode(releasePosition);

                // Only allowed to attach vines to settlements, other nodes can be the origin for a vine, but not the destination
                var settlement = closestNodeToReleasePos.transform.gameObject.GetComponent<Settlement>(); // does this mean we should search for our closest settlement instead of node?
                if (settlement != null)
                {
                    CreateEdge(closestNodeToClickPos, closestNodeToReleasePos);

                    // Spawn a bud
                    var bud = Instantiate(budPrefab);
                    bud.transform.position = closestNodeToReleasePos.transform.position;
                }

                // Hide the temporary line, final rendering is handled by the node class
                tempLineRenderer.positionCount = 0;

                closestNodeToClickPos = null;
                isMouseDown = false;
            }
        }

        //TODO
        //CAN BE MOVED LATER IF NEEDED, WAS JUST TESTING.
        if (Input.GetMouseButtonDown((int)MouseButton.RightClick))
        {
            Vector3 mouseWorldPosition = Util.GetMousePositionInWorldSpace();
            Node target = Util.FindClosestNode(mouseWorldPosition);
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
}
