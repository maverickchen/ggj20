using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public int level = 1;
    public int antiValue = 0; // the amount this log will decrement house branch count upon being dropped into the trash can

    public Sprite level1Sprite;
    public Sprite level2Sprite;
    public Sprite level3Sprite;
    SpriteRenderer spriteRenderer;
    List<Sprite> sprites;

    public void PickedUp()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Awake()
    {
        sprites = new List<Sprite>() {
            level1Sprite,
            level2Sprite,
            level3Sprite,
        };
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[level];
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        spriteRenderer.sprite = sprites[newLevel];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
