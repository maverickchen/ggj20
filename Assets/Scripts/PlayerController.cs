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
    public float beaverSpeed = .3f;
    public float beaverCarrySpeed = .1f;
    public float zookeeperSpeed = .15f;
    public float zookeeperCarrySpeed = .07f;
    float carrySpeed;
    float speed;
    public GameObject heldBranch; // the Branch GameObject being held by the player; null if player is emptyhanded
    Animator anim;
    SpriteRenderer sprite;

    public Sprite beaverSprite;
    public Sprite zookeeperSprite;
    public GameObject branchPrefab;
    public AudioSource pickupSound;
    public AudioSource beaverWalkSound;
    public AudioSource zookeeperWalkSound;
    public GameObject branchIcon;
    private AudioSource walkSound;

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
            walkSound = zookeeperWalkSound;
            speed = zookeeperSpeed;
            carrySpeed = zookeeperCarrySpeed;
        }
        else
        {
            Debug.Log("Beaver");
            role = PLAYER_TYPE.BEAVER;
            anim.SetBool("IsBeaver", true);
            sprite.sprite = beaverSprite;
            walkSound = beaverWalkSound;
            speed = beaverSpeed;
            carrySpeed = beaverCarrySpeed;
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
            branchIcon.SetActive(false);
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
                        heldBranch = Instantiate(branchPrefab, transform.position, transform.rotation);
                        heldBranch.SetActive(false);
                        Branch branchData = heldBranch.GetComponent<Branch>();
                        branchData.SetLevel(2);
                        branchData.antiValue = 2;
                        anim.SetBool("HasBranch", true);
                        pickupSound.PlayOneShot(pickupSound.clip, 20f);
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
                pickupSound.PlayOneShot(pickupSound.clip, 20f);
                if (role == PLAYER_TYPE.BEAVER)
                {
                    branchIcon.GetComponent<SpriteRenderer>().sprite = heldBranch.GetComponent<SpriteRenderer>().sprite;
                    branchIcon.SetActive(true);
                }
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
        bool isWalking = (input.magnitude >= 0.025f);
        if (!anim.GetBool("IsWalking") && isWalking)
        {
            walkSound.Play();
        }
        if (anim.GetBool("IsWalking") && !isWalking)
        {
            walkSound.Stop();
        }
        anim.SetBool("IsWalking", isWalking);

        Vector3 moveDir = new Vector3(input.x, 0f, input.y);
        sprite.flipX = (moveDir.x < 0);
        float currSpeed = (heldBranch == null) ? speed : carrySpeed;
        rb.MovePosition(rb.position + moveDir * currSpeed);
    }
}
