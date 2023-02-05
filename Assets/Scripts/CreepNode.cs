using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepNode : Node
{
    [SerializeField]
    private int infectPerNode = 2;

    [SerializeField]
    private Color vineColor = Color.magenta;

    [SerializeField]
    private float waitBeforeSpread = 1f;

    [SerializeField]
    private float infectionRadius = 10f;

    [SerializeField]
    private float creepTickDamage = 5f;

    private void Awake()
    {
        HealthSystem = gameObject.GetComponentInChildren<HealthSystem>(); // TODO: 8305
        edgeColor = vineColor;
        VineMaterialName = "CreepVine";
    }

    private void Start()
    {
        Infect();
    }

    public void Infect()
    {
        StartCoroutine(InfectionStart());
    }

    private IEnumerator InfectionStart()
    {
        while (GameManager.Instance.IsGamePaused) yield return null;

        yield return new WaitForSeconds(waitBeforeSpread);

        var nodesToDamage = FindClosestNodes();
        foreach (var n in nodesToDamage)
        {
            if (n is Settlement s)
            {
                s.StartDamageTick(creepTickDamage);
                s.HealthSystem.prefabHealthBar.SetActive(true);
                CreateEdge(this, s);
            }
            else if (n is PowerPlant p)
            {
                p.BecomeInfected();
                CreateEdge(this, p);
            }
        }
    }

    private Node[] FindClosestNodes()
    {
        // FIXME: we probably should copy less arrays around
        int found = 0;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, infectionRadius);
        //Array.Sort(hitColliders, (a, b) => (int)Mathf.Sign(Vector3.Distance(transform.position, b.transform.position) - Vector3.Distance(transform.position, a.transform.position)));

        var nodesToReturn = new List<Node>();

        for (int i = hitColliders.Length - 1; i >= 0; i--)
        {
            if(found>= infectPerNode)
            {
                break;
            }
            var settlement = hitColliders[i].GetComponentInChildren<Settlement>();
            if (settlement != settlement.isInfected)
            {
                nodesToReturn.Add(settlement);
                found++;
            }
        }

        return nodesToReturn.ToArray();
    }

    private void CreateEdge(Node n1, Node n2)
    {
        // The graph is bidirectional
        n1.AddAdjacentNode(n2, this);
        n2.AddAdjacentNode(n1, this);
    }
}
