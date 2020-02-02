using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanArea : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip trashcanSound;
    public HouseManager houseManager;
    private ArrayList collidingObjects = new ArrayList();

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (collidingObjects.Count > 0)
        {
            // keep lighting up outlines
            Debug.Log("light up outlines");
            Color color = new Color(255f, 0f, 0f, (Mathf.Sin(Time.time) + 1) / 2 * 255f);
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<MeshRenderer>().material.color = color;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        Branch branch = collidedObject.GetComponent<Branch>();
        if (branch != null)
        {
            // tell the house manager how much to decrement branchCount by
            houseManager.DestroyHouse(branch.antiValue);
            // remove the branch prefab
            Destroy(collidedObject);

            // play trash can sound
            audioSource.PlayOneShot(trashcanSound, 10f);
            
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
}
