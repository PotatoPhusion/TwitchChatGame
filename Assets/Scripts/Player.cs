using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public NavMeshAgent navAgent;
    [Tooltip("The time in minutes before the player is removed from the game" +
        "for being idle.")]
    public int timeoutThreshold;

    private string username;
    public string Username {
        get {
            return username;
        }
        set {
            if (username == null)
            {
                username = value;
            }
        }
    }
    private string message;
    public string Message {
        set {
            message = value;
        }
    }

    private Rigidbody rb;

    private float idleTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        idleTime += Time.deltaTime;

        if (idleTime >= timeoutThreshold * 60)
        {
            GameManager.Instance.RemovePlayer(Username);
        }
    }

    void LateUpdate()
    {
        ProcessInputs();
    }

    /// <summary>
    /// Processes the last message sent to this player to determine
    /// if any valid commands were sent. If a valid command is found, 
    /// that command is then executed.
    /// </summary>
    private void ProcessInputs() {
        if (message == null)
        {
            return;
        }

        if (message.Equals("idle"))
        {
            rb.MovePosition(transform.position + Vector3.left);
        }
        else if (message.Equals("mine"))
        {
            navAgent.SetDestination(GameManager.Instance.locations[0].entryPoint.position);
            // Add this player to the total number of miners and move the
            // player to the mines.
        }
        else if (message.Equals("forge"))
        {
            rb.MovePosition(transform.position + Vector3.forward);
        }
        else if (message.Equals("farm"))
        {
            rb.MovePosition(transform.position + Vector3.back);
        }
        else if (message.Equals("uwu"))
        {
            print("What's this?");
        }

        idleTime = 0.0f;
        message = null;
    }

    /// <summary>
    /// Send a message to this player. Usually from Twitch chat.
    /// </summary>
    /// <param name="message">The message for this player to process</param>
    public void SetMessage(string message) {
        this.message = message;
    }
}
