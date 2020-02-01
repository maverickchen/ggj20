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

    void numBranchesChanged()
    {
        // spawn appropriate houses
        if (branchCount == house0_branches)
        {
            Instantiate(House0);
            GameStateManager.instance.ZookeeperWon();
            Destroy(House1);
        }
        else if (branchCount == house1_branches)
        {
            Instantiate(House1);
            Destroy(House0);
            Destroy(House2);
        }
        else if (branchCount == house2_branches)
        {
            Instantiate(House2);
            Destroy(House1);
        }
    }

    public void BranchDropped()
    {
        Debug.Log("branch dropped");
        branchCount += 1;
        numBranchesChanged();
    }

    public void DestroyHouse()
    {
        branchCount -= branchesToDestroy;
        numBranchesChanged();
    }
}
