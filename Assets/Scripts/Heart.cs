using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    private Animator ani;
    private SpriteRenderer sr;
    private Color originalColor;
    public float BlinkDelay;
    public Color BlinkColor;
    private float lastBlink;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        ani.SetInteger("Qtd", 2);
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time - BlinkDelay > lastBlink) {
            sr.color = BlinkColor;
            lastBlink = Time.time;
        }
        float x = Time.time - lastBlink;
        float p = x / BlinkDelay;
        sr.color = new Color(BlinkColor.r * (1 - p) + originalColor.r * p, BlinkColor.g * (1 - p) + originalColor.g * p, BlinkColor.b * (1 - p) + originalColor.b * p);
		if(ani.GetInteger("Qtd") < 2) {
            transform.Find("Heart r").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        } else {
            transform.Find("Heart r").gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
	}
    public void SetHeart(int qtd) {
        if(qtd >= 0 && qtd <= 2) {
            ani.SetInteger("Qtd", qtd);
        } else {
            ani.SetInteger("Qtd", 0);
        }
        
    }
}
