using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashAnimShader : MonoBehaviour
{
    [SerializeField] private Material material;
    // Start is called before the first frame update

    /*
     * Basic Logic: the trash can starts at a height of 1
     * when the animation starts, it goes up to 2
     * then it goes up to .85
     * then it goes up to 1
     * the speed of which is determined by anim speed
     * 
     * 
     * 
     */
    public float animSpeed = 1f;
    public bool animate = false;
    private float animHeight = 1f;
    private float minHeight = 2f;
    private float maxHeight = 0.85f;
    private float sign = 1f;
    private bool hitmaxheight = false;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (animate) {
            animHeight = Mathf.Clamp(animHeight + Time.deltaTime * animSpeed * sign, maxHeight, minHeight);
            if (!hitmaxheight) { 
                if (animHeight == minHeight)
                    sign = -1f;
                if (animHeight == maxHeight) {
                    sign = 1f;
                    hitmaxheight = true;
                }
            }
            else {
                if (animHeight >= 1f) {
                    hitmaxheight = false;
                    animate = false;
                    animHeight = 1f;
                }
            }
            material.SetFloat("_AnimHeight", animHeight);
        }
    }
}
