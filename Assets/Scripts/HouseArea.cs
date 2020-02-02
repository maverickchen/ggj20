using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseArea : MonoBehaviour
{
    public HouseManager houseManager;
    private ArrayList collidingObjects = new ArrayList();
    private GameObject blinker;

    // Start is called before the first frame update
    void Start()
    {
        blinker = this.transform.Find("Blinker").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (collidingObjects.Count > 0)
        {
            // keep lighting up outlines
            Debug.Log("light up outlines");
            blinker.GetComponent<HouseAreaShaderControl>().stopBlink = false;
        }
        else
        {
            blinker.GetComponent<HouseAreaShaderControl>().stopBlink = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        Branch branch = collidedObject.GetComponent<Branch>();
        if (branch)
        {
            Destroy(collidedObject);
            houseManager.BranchDropped(branch.level);
        }
        if (collidedObject.GetComponent<PlayerController>())
        {
            collidingObjects.Add(collidedObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.GetComponent<PlayerController>())
        {
            collidingObjects.Remove(collidedObject);
        }
    }

    public void DestroyHouse(int numToDecrBy)
    {
        houseManager.DestroyHouse(numToDecrBy);
    }
}
