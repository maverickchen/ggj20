using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanArea : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip trashcanSound;
    public HouseManager houseManager;
    private ArrayList collidingObjects = new ArrayList();
    private GameObject blinker;
    private TrashAnimShader shader;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        blinker = this.transform.Find("Blinker").gameObject;
        shader = GameObject.Find("TrashCan").GetComponent<TrashAnimShader>();
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
        if (branch != null)
        {
            shader.animate = true;
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
