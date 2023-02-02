using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CreepNode : MonoBehaviour
{
    public float CreepTickDamage = 5f;
    //HealthScript HealthScript;

    private Node[] nodes;
    public void InfectNode()
    {
        nodes = Util.FindAllClosestNodes(transform.position);
        nodes[0].HealthManager.TickFor(CreepTickDamage);
        nodes[1].HealthManager.TickFor(CreepTickDamage);
    }

}
