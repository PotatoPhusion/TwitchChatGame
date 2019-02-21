using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatProcessor : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        if (TwitchChat.lastMessage == null)
        {
            return;
        }

        string message = TwitchChat.lastMessage;
        int splitPoint = message.IndexOf(":", 1);
        string command = message.Substring(splitPoint + 2, message.Length - (splitPoint + 2));
        string username = message.Substring(0, splitPoint);

        if (command.Equals("!join"))
        {
            GameManager.Instance.AddPlayer(username);
        }
        else
        {
            if (GameManager.Instance.FindPlayer(username))
            {
                Player player = GameManager.Instance.GetPlayer(username);
                player.SetMessage(command);
            }
        }
    }
}
