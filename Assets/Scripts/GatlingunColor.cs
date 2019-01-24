using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingunColor : MonoBehaviour {
    private SpriteRenderer sprite;
    private Color original;
    public float changeDelay;
    public Color color1;
    public Color color2;
    private float lastChange;
    private int parity;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        original = sprite.color;
        lastChange = Time.time;
        parity = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastChange > changeDelay)
        {   
            parity = (parity + 1) % 3;
            if(parity == 0)
            {
                sprite.color = color1;
            }
            if(parity == 1)
            {
                sprite.color = color2;
            }
            if(parity == 2)
            {
                sprite.color = original;
            }
            lastChange = Time.time;
        }

	}
}
