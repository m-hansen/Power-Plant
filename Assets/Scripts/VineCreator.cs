using UnityEngine;
using static UnityEngine.UI.Image;

public class VineCreator : MonoBehaviour
{
    [SerializeField]
    private LineRenderer tempLineRenderer;

    [SerializeField, Range(1, 100)]
    private int vineElasticity = 20;

    [SerializeField, Tooltip("This is used as a buffer to allow misclicks when dragging vines.")]
    private float nodeSnapDistance;

    private Node originNode = null;
    private bool isVineCreationInProgress = false;

    private void Update()
    {
        HandleInput();

        if (isVineCreationInProgress)
        {
            // Draw a temporary line to the cursor position
            tempLineRenderer.SetPosition(tempLineRenderer.positionCount - 1, Util.GetMousePositionInWorldSpace());

            float distance = (Util.GetMousePositionInWorldSpace() - originNode.transform.position).magnitude;
            tempLineRenderer.material.mainTextureScale = new Vector2(distance / vineElasticity, tempLineRenderer.material.mainTextureScale.y);
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnterVineCreationMode();
        }

        if (Input.GetMouseButtonUp(0) && isVineCreationInProgress)
        {
            ExitVineCreationMode();
        }
    }

    //hacky way to change depth to start from 5 and count down based on depth
    private void StupidVineThingINeed(Node originNode,Node destinationNode)
    {
        var settlementScript = destinationNode.GetComponent<Settlement>();
        if (!settlementScript.isInfected)
        {
            //can clamp node depth hp/s values here
            destinationNode.GetComponentInChildren<Settlement>().AddToResource((int)Mathf.Clamp(4 + -originNode.Depth, 0f, 99f));
            destinationNode.HealthSystem.StartHotHealing(Mathf.Clamp(5 - originNode.Depth, -99f, 99f));
            //n2.HealthSystem.StartHotHealing(Mathf.Clamp(5 - n1.Depth, -99f, 99f));
        }
    }

    private void EnterVineCreationMode()
    {
        // Find the node nearest the mouse click
        Vector3 mouseWorldPosition = Util.GetMousePositionInWorldSpace();
        originNode = Util.FindClosestNode(mouseWorldPosition);

        float misclickDistance = Vector3.Distance(mouseWorldPosition, originNode.transform.position);
        if (misclickDistance < nodeSnapDistance)
        {
            // Show a temporary line to the mouse position
            tempLineRenderer.positionCount = 2;
            tempLineRenderer.SetPosition(0, originNode.transform.position);

            isVineCreationInProgress = true;
        }
    }

    private void ExitVineCreationMode()
    {
        // Link two nodes
        Vector3 releasePosition = Util.GetMousePositionInWorldSpace();
        Node destinationNode = Util.FindClosestNode(releasePosition);

        if (IsValidVineConnection(originNode, destinationNode))
        {
            ConnectVineToNode(destinationNode);
        }

        // Hide the temporary line, final rendering is handled by the node class
        tempLineRenderer.positionCount = 0;

        originNode = null;
        isVineCreationInProgress = false;
    }

    private bool IsValidVineConnection(Node origin, Node destination)
    {
        var originSettlement = origin.GetComponent<Settlement>();
        var destinationSettlement = destination.GetComponent<Settlement>();

        var CreepOrigin = origin.GetComponent<CreepNode>();
        if (CreepOrigin)
        {
            return true;
        }

        // Cannot connect to itself
        if (origin == destination)
        {
            Debug.Log("Unable to connect a Vine. The origin and destination are the same node.");
            return false;
        }

        // The destination node must be a settlement
        if (destinationSettlement == null)
        {
            Debug.Log("Unable to connect a Vine. The destination is not a Settlement.");
            return false;
        }

        // The origin node must be the power plant or a settlement that is connected to the power plant in some way
        if (origin.GetComponent<PowerPlant>() == null && (originSettlement != null && !originSettlement.IsConnectedToPowerPlant()))
        {
            Debug.Log("Unable to connect a Vine. The originating Settlement is not connected to the Power Plant.");
            return false;
        }

        // The power plant has enough resources to pay the destination settlement's cost
        if (GameManager.Instance.Player.PrimaryResource < destinationSettlement.Cost)
        {
            Debug.Log("Unable to connect a Vine. You do not have enough resources.");
            return false;
        }

        // The destination node must not already have connections attached to it
        if (destination.EdgeCount > 0)
        {
            Debug.Log("Unable to connect a Vine. The destination already has connections.");
            return false;
        }

        Debug.Log($"Vine connected: [{origin.name}] -> [{destination.name}]");

        return true;
    }

    private void ConnectVineToNode(Node destination)
    {
        // Pay for the vine
        var settlementScript = destination.GetComponent<Settlement>();
        GameManager.Instance.Player.ExpendResource(settlementScript.Cost);

        //Check if destination has resource
        //If it does add resource per second
        if (settlementScript.hasResource)
        {
            GameManager.Instance.Player.AddResourcePerSecond(settlementScript.GetResourcePerSecond());
        }

        // Update the data structures
        CreateEdge(originNode, destination);
    }

    private void CreateEdge(Node n1, Node n2)
    {
        // The graph is bidirectional
        n1.AddAdjacentNode(n2, this);
        n2.AddAdjacentNode(n1, this);
        StupidVineThingINeed(n1, n2);

    }
}
