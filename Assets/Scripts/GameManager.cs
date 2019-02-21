using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public GameObject playerPrefab;

    public List<Location> locations;

    // Players should be removed after afk for some time
    // (~20 mins)?
    private Dictionary<string, Player> players;

    public int ore;

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        players = new Dictionary<string, Player>();
        locations = new List<Location>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds a new player to the game.
    /// </summary>
    /// <param name="username">The username of the player being added</param>
    public void AddPlayer(string username) {
        if (FindPlayer(username))
        {
            Debug.LogError("The player " + username + " already exists!");
            return;
        }

        GameObject newPlayerGO = Instantiate(playerPrefab, new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        Player newPlayer = newPlayerGO.GetComponent<Player>();
        newPlayer.Username = username;
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        newPlayerGO.GetComponent<MeshRenderer>().material.color = new Color(r, g, b);
        players.Add(username, newPlayer);
    }

    /// <summary>
    /// Removes a player from the game.
    /// </summary>
    /// <param name="username">The name of the player to remove</param>
    public void RemovePlayer(string username) {
        Destroy(GetPlayer(username).gameObject);
        players.Remove(username);
    }

    /// <summary>
    /// Check if a player exists in the player Dictionary.
    /// </summary>
    /// <param name="username">The player name to search for</param>
    /// <returns>True if player is found, otherwise false.</returns>
    public bool FindPlayer(string username) {
        return players.ContainsKey(username);
    }

    /// <summary>
    /// Retrieves a player from the Dictionary.
    /// </summary>
    /// <param name="username">The player name to search for</param>
    /// <returns>The requested player if it exists. If there is no player
    /// <i>username</i> then it returns null.</returns>
    public Player GetPlayer(string username) {
        if (!FindPlayer(username))
        {
            return null;
        }
        else
        {
            return players[username];
        }
    }

    /// <summary>
    /// Get the total number of registered players for the session.
    /// </summary>
    /// <returns>The number of current players</returns>
    public int GetPlayerCount() {
        return players.Count;
    }
}
