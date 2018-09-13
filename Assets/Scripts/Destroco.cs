using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroco : MonoBehaviour {
    private EdgeCollider2D ec;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float angle;
    public float speed;
    public float timerDeath;
    private float timer;
    public GameObject destExplosion;
    public float deathDelay;
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
        ec = GetComponent<EdgeCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        angle = Random.Range(3 * Mathf.PI / 4, 5 * Mathf.PI / 4);
        Vector2 spd = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
       // spd = new Vector2(-1, 0);
        spd = spd * speed * Random.Range(0.4f, 1.5f);
        rb.velocity = spd;
        timer = 0;
        sr.color = new Color(Random.Range(0.3f, 5.0f), Random.Range(0.05f, 0.8f), Random.Range(0.05f, 1.5f));
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
        var main = exp.GetComponent<ParticleSystem>().main;
        main.startColor.gradient.colorKeys[0].color = sr.color;
        sr.enabled = false;
        ec.enabled = false;
        Destroy(gameObject, deathDelay);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Bullet") {
            if (collision.gameObject.GetComponent<Rigidbody2D>().mass < 20) {
                audioManager.PlaySound("shoot meteor");
            } else {
                audioManager.PlaySound("shoot 2 meteor");
            }
            AutoDestroy();
        }
        if(collision.gameObject.tag == "Player") {
            var speed = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y);
            if(speed > 4) {
                collision.gameObject.GetComponent<Player>().TakeDamage(1);
                AutoDestroy();
            }
            audioManager.PlaySound("meteor col");
        }
    }
}
