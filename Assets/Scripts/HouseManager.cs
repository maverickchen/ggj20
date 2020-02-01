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

    public Sprite House0;
    public Sprite House1;
    public Sprite House2;

    public AudioSource woodAddedSound;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        branchCount = 3f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        numBranchesChanged();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void numBranchesChanged()
    {
        if (branchCount >= house2_branches)
        {
            spriteRenderer.sprite = House2;
        }
        else if (branchCount >= house1_branches)
        {
            spriteRenderer.sprite = House1;
        }
        else if (branchCount > house0_branches)
        {
            spriteRenderer.sprite = House0;
        }

        if (branchCount == house0_branches)
        {
            GameStateManager.instance.ZookeeperWon();
        }

        //// spawn appropriate houses
        //if (branchCount == house0_branches)
        //{
        //    spriteRenderer.sprite = House0;
        //    GameStateManager.instance.ZookeeperWon();
        //    Destroy(House1);
        //}
        //else if (branchCount == house1_branches)
        //{
        //    spriteRenderer.sprite = House1;
        //    Destroy(House0);
        //    Destroy(House2);
        //}
        //else if (branchCount == house2_branches)
        //{
        //    Instantiate(House2);
        //    Destroy(House1);
        //}
    }

    public void BranchDropped()
    {
        Debug.Log("branch dropped");

        branchCount += 1;
        numBranchesChanged();
        woodAddedSound.PlayOneShot(woodAddedSound.clip, 4f);
        Debug.Log(branchCount.ToString() + " branches in house");
    }

    public void DestroyHouse()
    {
        branchCount -= branchesToDestroy;
        numBranchesChanged();
        Debug.Log(branchCount.ToString() + " branches in house");
    }
}
