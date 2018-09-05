using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroco : MonoBehaviour {
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float angle;
    public float speed;
	// Use this for initialization
	void Start () {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        angle = Random.Range(3 * Mathf.PI / 4, 5 * Mathf.PI / 4);
        Vector2 spd = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
       // spd = new Vector2(-1, 0);
        spd = spd * speed;
        rb.velocity = spd;

	}
	
	// Update is called once per frame
	void Update () {

        
	}
}
