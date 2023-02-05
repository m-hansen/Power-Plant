using UnityEngine;

public class LineTesting : MonoBehaviour
{
    public Transform target;

    private LineRenderer lr;
    private Renderer rend;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rend = lr.GetComponent<Renderer>();
        lr.positionCount = 2;
    }

    void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(0, target.position);

        float distance = (target.position - transform.position).magnitude;
        rend.material.mainTextureScale = new Vector2(3/ distance, rend.material.mainTextureScale.y);
    }
}
