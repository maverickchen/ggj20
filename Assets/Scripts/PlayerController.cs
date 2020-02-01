using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum PLAYER_TYPE
    {
        BEAVER = 1,
        ZOOKEEPER = 2,
    }
    public PLAYER_TYPE role;
    public int playerIndex;
    Vector2 movement;
    Rigidbody rb;
    public float speed = 10f;
    public GameObject heldBranch; // the Branch GameObject being held by the player; null if player is emptyhanded
    Animator anim;
    SpriteRenderer sprite;
    public Sprite beaverSprite;
    public Sprite zookeeperSprite;
    public GameObject branchPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerIndex = GameStateManager.instance.RegisterPlayer(gameObject);
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if (playerIndex == 0)
        {
            Debug.Log("Zookeeper");
            role = PLAYER_TYPE.ZOOKEEPER;
            anim.SetBool("IsBeaver", false);
            sprite.sprite = zookeeperSprite;
        }
        else
        {
            Debug.Log("Beaver");
            role = PLAYER_TYPE.BEAVER;
            anim.SetBool("IsBeaver", true);
            sprite.sprite = beaverSprite;
        }
        Debug.Log("Player " + playerIndex.ToString() + " registered");
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnToggleBranch()
    {
        if (heldBranch != null) // player has a branch, put it down
        {
            Vector3 offset = new Vector3(0f, 2f, 0f);
            heldBranch.transform.position = transform.position + offset;
            heldBranch.SetActive(true);
            heldBranch = null;
            anim.SetBool("HasBranch", false);
        }
        else // player not holding a branch, try to pick one up
        {
            if (role == PLAYER_TYPE.ZOOKEEPER)
            // Zookeepers can pull logs out from the beaver house
            {
                float radius = 2f;
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider collider in colliders)
                {
                    HouseArea houseArea = collider.gameObject.GetComponent<HouseArea>();
                    
                    if (houseArea != null)
                    {
                        houseArea.DestroyHouse();
                        heldBranch = Instantiate(branchPrefab, transform.position, transform.rotation);
                        heldBranch.SetActive(false);
                        heldBranch.GetComponent<Branch>().SetLevel(2);
                        anim.SetBool("HasBranch", true);
                        return; // if we pull it out from the beaver house, we are done
                    }
                }
            }
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
        float radius = 3f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            GameObject obj = collider.gameObject;
            if (obj.GetComponent<Branch>())
            {
                return obj;
            }
        }
        return null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 input = Vector2.ClampMagnitude(movement, 1f);
        anim.SetBool("IsWalking", (input.magnitude >= 0.025f));
        Vector3 moveDir = new Vector3(input.x, 0f, input.y);
        sprite.flipX = (moveDir.x < 0);

        rb.MovePosition(rb.position + moveDir * speed);
    }
}
