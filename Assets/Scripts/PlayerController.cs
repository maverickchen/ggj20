using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int playerIndex;
    Vector2 movement;
    Rigidbody rb;
    public float speed = 10f;
    public GameObject heldBranch; // the Branch GameObject being held by the player; null if player is emptyhanded
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerIndex = GameStateManager.instance.RegisterPlayer(gameObject);
        Debug.Log("Player " + playerIndex.ToString() + " registered");
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnToggleBranch()
    {
        if (heldBranch != null) // player has a branch
        {
            anim.SetBool("HasBranch", true);
        }
        else // player not holding a branch, try to pick one up
        {
            // get the nearest branch object 
            GameObject nearestBranch = GetNearestBranch();
            if (nearestBranch != null)
            {
                heldBranch = nearestBranch;
                nearestBranch.SetActive(false); // make the branch invisible to players
                anim.SetBool("HasBranch", true);
            }
        }
    }

    GameObject GetNearestBranch()
    {
        return null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(movement.x, 0f, movement.y).normalized;
        rb.MovePosition(rb.position + moveDir * speed);
    }
}
