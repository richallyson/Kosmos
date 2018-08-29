using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed;
    public GameObject bullet;
    public float bulletSpeed;
    private SpriteRenderer sr;
    public float DashSpeed;
    public float DashTime;
    public bool Dashing;
    public int DashDirection;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update() {
        if (Dashing) {
            //if(DashDirection == 0) {
                //<rb.velocity = new Vector2(DashSpeed*Mathf.Cos())
            //}

        } else { 
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0) {
                float angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
                rb.velocity = new Vector2(Mathf.Cos(angle) * Mathf.Abs(Input.GetAxis("Horizontal")) * speed, Mathf.Sin(angle) * Mathf.Abs(Input.GetAxis("Vertical")) * speed);
                Flip(Input.GetAxis("Horizontal"));
            } else {
                rb.velocity = new Vector2(0, 0);
            }
        }
        if (Input.GetButtonDown("Fire1")) {
            Shoot();

        }
    }
    void Shoot() {
        GameObject b = Instantiate(bullet);
        
        if (sr.flipX) {
            b.transform.position = new Vector3(transform.Find("bulletPosition").localPosition.x + transform.position.x, transform.Find("bulletPosition").position.y, 0);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
        } else {
            b.transform.position = new Vector3(transform.position.x - transform.Find("bulletPosition").localPosition.x, transform.Find("bulletPosition").position.y, 0);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
        }
    }
    void Flip(float n) {
        if(n < 0) {
            sr.flipX = false;
        }
        if(n > 0) {
            sr.flipX = true;
        }
    }
    void Dash() {


    }
}
