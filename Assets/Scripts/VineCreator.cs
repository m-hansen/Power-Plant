using UnityEngine;

public class VineCreator : MonoBehaviour
{
    [SerializeField]
    private LineRenderer tempLineRenderer;

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
        var settlement = destination.GetComponent<Settlement>();

        // The destination node must be a settlement
        if (settlement == null)
        {
            Debug.Log("Unable to connect a Vine. The destination is not a Settlement.");
            return false;
        }

        // The origin node must be the power plant or a settlement that is connected to the power plant in some way
        var originSettlement = origin.GetComponent<Settlement>();
        if (origin.GetComponent<PowerPlant>() == null && (originSettlement != null && !originSettlement.IsConnectedToPowerPlant()))
        {
            Debug.Log("Unable to connect a Vine. The originating Settlement is not connected to the Power Plant.");
            return false;
        }

        // The power plant has enough resources to pay the destination settlement's cost
        if (GameManager.Instance.Player.PrimaryResource < settlement.Cost)
        {
            Debug.Log("Unable to connect a Vine. You do not have enough resources.");
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
        destination.HealthSystem.StartHotHealing(1*-destination.GetDepth());

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
        n1.AddAdjacentNode(n2);
        n2.AddAdjacentNode(n1);
    }
}
