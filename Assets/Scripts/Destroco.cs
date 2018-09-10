using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroco : MonoBehaviour {
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float angle;
    public float speed;
    public float timerDeath;
    private float timer;
    public GameObject destExplosion;
    public float deathDelay;
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
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= timerDeath) {
            AutoDestroy();
        }
	}
    public void AutoDestroy() {
        GameObject exp = Instantiate(destExplosion);
        exp.transform.position = transform.position;
        sr.enabled = false;
        bc.enabled = false;
        Destroy(gameObject, deathDelay);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Bullet") {
            AutoDestroy();
        }
    }
}
