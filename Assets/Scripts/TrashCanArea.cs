using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanArea : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip trashcanSound;

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
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedBranch = collision.gameObject;
        if (collidedBranch.GetComponent<Branch>() != null)
        {
            // remove the branch prefab
            Destroy(collidedBranch);

            // play trash can sound
            audioSource.PlayOneShot(trashcanSound, 10f);
        }
    }
}
