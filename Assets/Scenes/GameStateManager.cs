using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeaversWon()
    {
        // display "Beavers Won!" message
        Debug.Log("beavers won!");
    }

    public void ZookeeperWon()
    {
        // display message
        Debug.Log("Zookeeper won!");
    }
}
