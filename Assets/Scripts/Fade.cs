using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
    private SpriteRenderer sr;
    private Color original;
    public Gradient fade;
    public float start;
    public float duration;
    private float timer;
    public bool done;
    // Use this for initialization
	void Start () {
        timer = 0;
        done = false;
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > start && timer < duration) {
            sr.color = fade.Evaluate((timer - start)/duration);
        }
        if(timer >= duration) {
            done = true;
        }
	}
}
