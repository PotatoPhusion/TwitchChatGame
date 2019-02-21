using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[System.Serializable]
public abstract class Location : MonoBehaviour
{
    public string locationName;
    public int locationIndex;
    public Transform entryPoint;
    protected List<Player> localPlayers;

    void Awake()
    {
        localPlayers = new List<Player>();
    }

    void Start() {
        GameManager.Instance.locations.Insert(locationIndex, this);
    }

    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerExit(Collider other);
}
