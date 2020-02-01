using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;
    List<GameObject> players;

    void Awake()
    {
        players = new List<GameObject>();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
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
