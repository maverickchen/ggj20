using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseArea : MonoBehaviour
{
    public HouseManager houseManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedBranch = collision.gameObject;
        if (collidedBranch.GetComponent<Branch>())
        {
            Destroy(collidedBranch);
            houseManager.BranchDropped();
        }
    }

    public void DestroyHouse()
    {
        houseManager.DestroyHouse();
    }
}
