using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject beaverWonGraphic;
    public GameObject zookeeperWonGraphic;
    public static GameStateManager instance;
    List<GameObject> players;

    List<Vector3> playerSpawnPositions = new List<Vector3>()
    {
        new Vector3(-6f, 0f, 1.5f),
        new Vector3(-3f, 0f, 1.5f),
        new Vector3(0f, 0f, 1.5f),
        new Vector3(3f, 0f, 1.5f),
    };

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
        player.transform.position = playerSpawnPositions[players.Count - 1];
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
        beaverWonGraphic.SetActive(true);
        Debug.Log("beavers won!");
    }

    public void ZookeeperWon()
    {
        // display message
        zookeeperWonGraphic.SetActive(true);
        Debug.Log("Zookeeper won!");
    }
}
