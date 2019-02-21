using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : StrategicLocation
{
    public float scaling;

    private float ore;
    private float carry;

    protected override void GenerateResource()
    {
        if (carry >= 1.0f)
        {
            ore += carry;
            carry = 0.0f;
        }
        ore += localPlayers.Count * scaling * Time.deltaTime;
        carry += ore - Mathf.Floor(ore);

        GameManager.Instance.ore += (int)ore;
        ore = 0.0f;
    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            localPlayers.Add(other.gameObject.GetComponent<Player>());
        }
    }

    protected override void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            localPlayers.Remove(other.gameObject.GetComponent<Player>());
        }
    }
}
