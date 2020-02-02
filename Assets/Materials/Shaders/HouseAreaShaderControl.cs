using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAreaShaderControl : MonoBehaviour {

    [SerializeField] private Material material;
    public float blinkRate = 0f;
    public Color color = Color.red;
    private float blinker = 0f;
    private float sign = 1f;
    public bool setColor = false;
    public bool stopBlink = false;

    // Start is called before the first frame update
    void Start() {
        material = GetComponent<SpriteRenderer>().material;
        material.SetColor("_BorderColor", color);
    }

    // Update is called once per frame
    void Update() {
        blinker = Mathf.Clamp(blinker + Time.deltaTime * blinkRate * sign, 0, 1);
        if (blinker == 1)
            sign = -1;
        else if 
            (blinker == 0) sign = 1;


        material.SetFloat("_blinker", blinker);

        if (setColor) {
            material.SetColor("_BoarderColor", color);
            setColor = false;
        }
        if (stopBlink)
        {
            material.SetFloat("_blinker", 1);
            stopBlink = false;
        }

    }

}
