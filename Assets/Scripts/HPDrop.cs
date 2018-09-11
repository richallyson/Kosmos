using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDrop : MonoBehaviour {
    private CircleCollider2D cc;
    private SpriteRenderer sr;
    private ParticleSystem ps;
    private Color originalColor;
    private float dying;
    public float pulsingRadius;
    public float pulsingFrequency;
    public float minRadius;
    public GameObject fire;
    public float Decay = 10.0f;
    private bool dead;
	// Use this for initialization
	void Start () {
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        originalColor = sr.color;
        minRadius += pulsingRadius;
	}
	
	// Update is called once per frame
	void Update () {
        if (!dead) {
            Decay -= Time.deltaTime;
            if (Decay < 0) {
                AutoDestroy();
            }
            float t = Time.time * pulsingFrequency;
            transform.localScale = new Vector3(minRadius + pulsingRadius * Mathf.Sin(t), minRadius + pulsingRadius * Mathf.Sin(t), minRadius + pulsingRadius * Mathf.Sin(t));
        }
        if (dead && dying > 0) {
            dying -= Time.deltaTime;
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, dying);
        }
    }
    private void AutoDestroy() {
        cc.enabled = false;
        dying = 1.0f;
        ps.Stop();
        dead = true;
        Destroy(gameObject, 1.0f);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            GameObject f = Instantiate(fire);
            collision.gameObject.GetComponent<Player>().Cure(2);
            AutoDestroy();
        }
    }
}
