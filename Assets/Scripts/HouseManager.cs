using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    private float branchCount;
    private float branchesToDestroy = 2f;

    public float house0_branches = 0f;
    public float house1_branches = 3f;
    public float house2_branches = 6f;

    public GameObject House0;
    public GameObject House1;
    public GameObject House2;

    // Start is called before the first frame update
    void Start()
    {
        branchCount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void numBranchesChanged(bool branchesAdded)
    {
        if (branchCount == house0_branches)
        {
            Instantiate(House0);
            Destroy(House1);
            GameStateManager.instance.ZookeeperWon();
        }
        else if (branchCount == house1_branches)
        {
            Instantiate(House1);
            if (branchesAdded)
            {
                Destroy(House0);
            } else
            {
                Destroy(House2);
            }
        }
        else if (branchCount == house2_branches)
        {
            Instantiate(House2);
            Destroy(House1);
        }
    }

    void BranchDropped()
    {
        branchCount += 1;
        numBranchesChanged(true);
    }

    void DestroyHouse()
    {
        branchCount -= branchesToDestroy;
        numBranchesChanged(false);
    }
}
