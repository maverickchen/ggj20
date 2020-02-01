using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public float branchCount;
    public float house0 = 0f;
    public float house1 = 3f;
    public float house2 = 6f;
    public float house3 = 9f;

    // Start is called before the first frame update
    void Start()
    {
        branchCount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (branchCount == house0)
        {
            GameStateManager gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
            gameStateManager.zookeeperWon();
        } else if (branchCount == house1)
        {

        }
    }

    void BranchDropped()
    {
        branchCount += 1;
    }
}
