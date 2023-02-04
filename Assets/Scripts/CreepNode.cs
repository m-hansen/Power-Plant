using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepNode : Node
{
    //[SerializeField]
    //private int infectPerNode = 2;

    [SerializeField]
    private Color vineColor = Color.magenta;

    [SerializeField]
    private float waitBeforeSpread = 0.5f;

    //[SerializeField]
    //private float infectionRadius = 5f;

    private float creepTickDamage = 5f;

    private void Awake()
    {
        HealthSystem = gameObject.GetComponentInChildren<HealthSystem>(); // TODO: 8305

        edgeColor = vineColor;
    }

    private void Start()
    {
        Infect();
    }

    public void Infect()
    {
        StartCoroutine(InfectionStart());
    }

    IEnumerator InfectionStart()
    {
        while (GameManager.Instance.IsGamePaused) yield return null;

        yield return new WaitForSeconds(waitBeforeSpread);

        var settlementsToDamage = FindSettlements();
        foreach (var settlement in settlementsToDamage)
        {
            settlement.StartDamageTick(creepTickDamage);
        }
    }

    private Settlement[] FindSettlements()
    {
        // FIXME: we probably should copy less arrays around

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 6f);
        Array.Sort(hitColliders, (a, b) => (int)Mathf.Sign(Vector3.Distance(transform.position, b.transform.position) - Vector3.Distance(transform.position, a.transform.position)));

        List<Settlement> settlementsToReturn = new List<Settlement>();

        for (int i = hitColliders.Length; i-- > 0;)
        {
            var settlement = hitColliders[i].GetComponentInChildren<Settlement>();
            if (settlement != null)
            {
                settlementsToReturn.Add(settlement);
            }
        }

        return settlementsToReturn.ToArray();
    }
}
