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
    private float DashCounter;
    public bool Dashing;
    public int DashDirection;

    public int energy;
    public int energyMax;
    public float energyTime;
    private float energyCounter;

    private GameObject Trail1;
    private GameObject Trail2;
    private GameObject Rastro1;
    private GameObject Rastro2;
    private float rastroRate;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Trail1 = transform.Find("Trail1").gameObject;
        Trail2 = transform.Find("Trail2").gameObject;
        Rastro1 = transform.Find("Rastro1").gameObject;
        Rastro2 = transform.Find("Rastro2").gameObject;
    }

    // Update is called once per frame
    void Update() {
        if (Dashing) {
            rb.velocity = new Vector2(DashSpeed * Mathf.Cos(DashDirection * Mathf.Deg2Rad), DashSpeed * Mathf.Sin(DashDirection * Mathf.Deg2Rad));
            DashCounter += Time.deltaTime;
            if (DashCounter > DashTime) {
                Dashing = false;
            }
            rastroRate = 30;
        } else {
            rastroRate = 10;
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
        if (Input.GetButtonDown("Dash")) {
            Dash();
        }
        if (energy < energyMax) {
            energyCounter += Time.deltaTime;
            if (energyCounter > energyTime) {
                energy++;
                energyCounter = 0;
            }
        }
        var emission = Trail1.GetComponent<ParticleSystem>().emission;
        emission.rateOverTimeMultiplier = Mathf.Min(energy, 1)*150;
        var emission2 = Trail2.GetComponent<ParticleSystem>().emission;
        emission2.rateOverTimeMultiplier = Mathf.Max(energy - 1, 0)*250;
        var rastroEmission = Rastro1.GetComponent<ParticleSystem>().emission;
        var rastroEmission2 = Rastro2.GetComponent<ParticleSystem>().emission;

        if (sr.flipX) {
            rastroEmission.rateOverTimeMultiplier = 0;
            rastroEmission2.rateOverTimeMultiplier = rastroRate;
        } else {
            rastroEmission.rateOverTimeMultiplier = rastroRate;
            rastroEmission2.rateOverTimeMultiplier = 0;
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
        if (energy > 0) {
            rb.velocity = new Vector2(0, 0);
            Dashing = true;
            energy --;
            DashCounter = 0;
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
                if (sr.flipX) {
                    DashDirection = 0;
                } else {
                    DashDirection = 180;
                }
            }
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0) {
                DashDirection = 0;
            }
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0) {
                DashDirection = 45;
            }
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") > 0) {
                DashDirection = 90;
            }
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0) {
                DashDirection = 135;
            }
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0) {
                DashDirection = 180;
            }
            if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0) {
                DashDirection = 225;
            }
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") < 0) {
                DashDirection = 170;
            }
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0) {
                DashDirection = 315;
            }
        }

    }
}
