using System;
using System.Collections;
using UnityEngine;

public class CreepNode : MonoBehaviour
{
    public float creepTickDamage = 5f;

    private Node obj;
    [SerializeField]
    private int infectPerNode = 2;


    [SerializeField]
    private float waitBeforeSpread = 0.5f;
    public float infectionRadius { get; private set; }

    private void Start()
    {
        Infect();
        infectionRadius = 5f;
    }
    public void Infect()
    {
        StartCoroutine(InfectionStart());
    }

    IEnumerator InfectionStart()
    {
        while (GameManager.Instance.gamePaused) yield return null;
        yield return new WaitForSeconds(waitBeforeSpread);
        FindNodes();
    }
    private void FindNodes()
    {
        int found = 0;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 6f);
        Array.Sort(hitColliders, (a, b) => (int)Mathf.Sign(Vector3.Distance(transform.position, b.transform.position) - Vector3.Distance(transform.position, a.transform.position)));
        for (int i = hitColliders.Length; i-- > 0;)
        {
            obj = hitColliders[i].GetComponentInChildren<Node>();
            if (found >= infectPerNode)
            {
                return;
            }
            else if (!obj.HealthSystem.IsInfected)
            {
                obj.HealthSystem.TickDamage(5);
                found++;
            }
        }
    }
}
