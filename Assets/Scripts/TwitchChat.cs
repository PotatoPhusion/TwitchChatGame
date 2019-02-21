using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchChat : MonoBehaviour
{

    public static string lastMessage;
    private static Queue<string> chatBuffer;

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    [HideInInspector]
    public string username, password, channelName; // Get the password from https://twitchapps.com/tmi

    // Start is called before the first frame update
    void Start()
    {
        chatBuffer = new Queue<string>();
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (!twitchClient.Connected)
        {
            Connect();
        }

        ReadChat();
        if (chatBuffer.Count > 0)
        {
            lastMessage = chatBuffer.Dequeue();
        }
        else
        {
            lastMessage = null;
        }
    }

    /// <summary>
    /// Connects to the Twitch IRC chat.
    /// </summary>
    private void Connect() {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();
    }

    /// <summary>
    /// Reads the most recent message from the Twitch IRC chat
    /// and adds it to the chat buffer.
    /// </summary>
    private void ReadChat() {
        if (twitchClient.Available > 0)
        {
            string message = reader.ReadLine(); // Read in the current message

            if (message.Contains("PRIVMSG"))
            {
                // Get the users name by spliting it from the string
                int splitPoint = message.IndexOf("!", 1);
                string chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                // Get the users message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                chatBuffer.Enqueue(String.Format("{0}: {1}", chatName, message));
                print(String.Format("{0}: {1}", chatName, message));
            }
            // Maintain connection to twitch chat
            if (message.Contains("PING"))
            {
                writer.WriteLine("PONG :tmi.twitch.tv");
                writer.Flush();
                print(message);
                print("PONG :tmi.twitch.tv");
            }
        }
    }
}
