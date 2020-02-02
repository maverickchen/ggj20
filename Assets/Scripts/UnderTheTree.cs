using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderTheTree : MonoBehaviour
{
    private float initialSpawnInterval = 1f;
    private float initialBranchesToSpawn = 5f;
    private float spawnInterval = 4f;
    private float branchesToSpawn = 2f;

    private float timer;
    private bool spawning;

    public GameObject Branch;

    private IEnumerator coroutine;

    private AudioSource audioSource;
    public AudioClip spawnSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnBranches(initialBranchesToSpawn);
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                spawnBranches(branchesToSpawn);
                timer = spawnInterval;
            }
        }
    }

    void spawnBranches(float numBranches)
    {
        spawning = true;

        // tree dimensions
        GameObject tree = GameObject.Find("Tree");
        Vector3 treeMidPosition = tree.transform.position;
        Vector3 treeSize = tree.GetComponent<Renderer>().bounds.size;
        float topOfTree = treeMidPosition.y + treeSize.y / 2;
        float treeZ = treeMidPosition.z - treeSize.z / 2;

        // branch dimensions

        Vector3 branchSize = Branch.GetComponent<Renderer>().bounds.size;
        float longestRadiusOfBranch = branchSize.x / 2;

        Vector3 midPosition = this.transform.position;
        Vector3 size = this.GetComponent<Renderer>().bounds.size;
        size = new Vector3(size.x - longestRadiusOfBranch, size.y - longestRadiusOfBranch, size.z - longestRadiusOfBranch);

        coroutine = spawn(midPosition, size, topOfTree, treeZ, numBranches);
        StartCoroutine(coroutine);
    }
    
    IEnumerator spawn(Vector3 midPosition, Vector3 size, float topOfTree, float treeZ, float numBranches)
    {
        int i = 0;
        while (i < numBranches)
        {
            yield return new WaitForSeconds(initialSpawnInterval);
            float randomX = Random.Range(midPosition.x - size.x / 2, midPosition.x + size.x / 2);
            float randomZ = Random.Range(midPosition.z - size.z / 2, treeZ);
            float randomRotation = Random.Range(0, 360);

            Vector3 spawnPosition = new Vector3(randomX, topOfTree, randomZ);

            Instantiate(Branch, spawnPosition, Quaternion.Euler(new Vector3(0, 0, randomRotation)));
            // play branch spawning sound
            audioSource.PlayOneShot(spawnSound, 0.5f);
            i++;
        }
        spawning = false;
    }
}
