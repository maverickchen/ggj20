using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject beaverWonGraphic;
    public GameObject zookeeperWonGraphic;
    public static GameStateManager instance;
    List<GameObject> players;
    public AudioSource winSound;
    public List<GameObject> playerCards;

    List<Vector3> playerSpawnPositions = new List<Vector3>()
    {
        new Vector3(-5.2f, 0f, -2.7f),
        new Vector3(-2.16f, 0f, -2.7f),
        new Vector3(1.16f, 0f, -2.7f),
        new Vector3(4.16f, 0f, -2.7f),
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
        playerCards[players.Count - 1].SetActive(true);
        if (players.Count == 3)
        {
            foreach (GameObject playerObject in players)
            {
                playerObject.GetComponent<PlayerController>().GameStarted();
            }
        }
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

    public void NotifyGameOver()
    {
        foreach (GameObject playerObject in players)
        {
            playerObject.GetComponent<PlayerController>().GameEnded();
        }
    }

    public void BeaversWon()
    {
        // display "Beavers Won!" message
        beaverWonGraphic.SetActive(true);
        winSound.Play();
        Debug.Log("beavers won!");
        NotifyGameOver();
    }

    public void ZookeeperWon()
    {
        // display message
        zookeeperWonGraphic.SetActive(true);
        winSound.Play();
        Debug.Log("Zookeeper won!");
        NotifyGameOver();
    }
}
