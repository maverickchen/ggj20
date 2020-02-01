using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerIndex;
    // Start is called before the first frame update
    void Start()
    {
        GameLogic.instance.RegisterPlayer(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
