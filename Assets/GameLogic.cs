using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    GameLogic game;
    List<GameObject> players;

    void Awake()
    {
        players = new List<GameObject>();
        if (game == null)
        {
            game = this;
        }
        else if (game != this)
        {
            Destroy(gameObject);
        }
    }

    public int RegisterPlayer(GameObject player)
    {
        players.Add(player);
        return players.Count - 1;
    }
}
