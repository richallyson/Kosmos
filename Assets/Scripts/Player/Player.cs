using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed;
    public GameObject bullet;
    public GameObject bullet2;
    public float bulletSpeed;
    private SpriteRenderer sr;
    private Animator ani;
    public float ShootDelay;
    private float shootCounter;
    public GameObject bigblink1;
    public GameObject bigblink2;

    public float DashSpeed;
    public float DashTime;
    private float DashCounter;
    public bool Dashing;
    public int DashDirection;

    public int energy;
    public int energyMax;
    public float energyTime;
    private float energyCounter;

    private GameObject Carga1;
    private GameObject Carga2;
    private GameObject Rastro1;
    private GameObject Rastro2;
    private float rastroRate;


    public int hp;
    public int MaxHp = 6;
    public int score;
    private HealthController hc;

    public bool hasControl;
    private float controlDelay;
    private float lastTimeControl;

    public int scoreMultiplier = 1;
    public Vector2[] scoreMultiplierSheet;
    private float playingTime = 0;

    private AudioManager audioManager;
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Carga1 = transform.Find("Carga1").gameObject;
        Carga2 = transform.Find("Carga2").gameObject;
        Rastro1 = transform.Find("Rastro1").gameObject;
        Rastro2 = transform.Find("Rastro2").gameObject;
        ani = GetComponent<Animator>();
        shootCounter = 0;
        hc = GameObject.Find("Health").GetComponent<HealthController>();
        hp = MaxHp;
        lastTimeControl = 0;
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update() {
        playingTime += Time.deltaTime;
        shootCounter += Time.deltaTime;
        if(Time.time - lastTimeControl > controlDelay) {
            hasControl = true;
        }
        if (Dashing) {
            rb.velocity = new Vector2(DashSpeed * Mathf.Cos(DashDirection * Mathf.Deg2Rad), DashSpeed * Mathf.Sin(DashDirection * Mathf.Deg2Rad));
            DashCounter += Time.deltaTime;
            if (DashCounter > DashTime) {
                Dashing = false;
            }
            rastroRate = 30;
        } else {
            rastroRate = 10;
            if (hasControl) {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0) {
                    float angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
                    rb.velocity = new Vector2(Mathf.Cos(angle) * Mathf.Abs(Input.GetAxis("Horizontal")) * speed, Mathf.Sin(angle) * Mathf.Abs(Input.GetAxis("Vertical")) * speed);
                    Flip(Input.GetAxis("Horizontal"));
                } else {
                    rb.velocity = new Vector2(0, 0);
                }
                if (shootCounter > ShootDelay) {
                    if (Input.GetButtonDown("Fire1")) {
                        Shoot1();
                    }
                    if (Input.GetButtonDown("Fire3")) {
                        Shoot2();
                    }
                }
                if (Input.GetButtonDown("Dash")) {
                    Dash();
                }
            }
        }

        if (energy < energyMax) {
            energyCounter += Time.deltaTime;
            if (energyCounter > energyTime) {
                energy++;
                energyCounter = 0;
            }
        }
        var emission = Carga1.GetComponent<ParticleSystem>().emission;
        emission.rateOverTimeMultiplier = Mathf.Min(energy, 1)*20;
        var emission2 = Carga2.GetComponent<ParticleSystem>().emission;
        emission2.rateOverTimeMultiplier = Mathf.Max(energy - 1, 0)*30;
        var rastroEmission = Rastro1.GetComponent<ParticleSystem>().emission;
        var rastroEmission2 = Rastro2.GetComponent<ParticleSystem>().emission;

        if (sr.flipX) {
            rastroEmission.rateOverTimeMultiplier = 0;
            rastroEmission2.rateOverTimeMultiplier = rastroRate;
        } else {
            rastroEmission.rateOverTimeMultiplier = rastroRate;
            rastroEmission2.rateOverTimeMultiplier = 0;
        }
        //Score Multiplier Sheet (SMS)
        //Essa parte do código é responsável por atualizar o SMS do player
        foreach (var i in scoreMultiplierSheet) {
            if (playingTime > i.x) {
                scoreMultiplier = (int) i.y;
            }
        }
        
    }

    public void OffControl(float delay) {
        lastTimeControl = Time.time;
        controlDelay = delay;
        hasControl = false;
    }
    void Shoot1() {
        audioManager.PlaySound("shot1");
        ani.SetTrigger("Shoot1");
        GameObject b = Instantiate(bullet);
        GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.04f);
        GameObject blink = Instantiate(bigblink1);
        if (sr.flipX) {
            b.transform.position = new Vector3(transform.Find("bulletPosition").localPosition.x + transform.position.x, transform.Find("bulletPosition").position.y, 0);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            blink.transform.position = new Vector3(transform.Find("bulletPosition").localPosition.x + transform.position.x, transform.Find("bulletPosition").position.y, 0);

        } else {
            b.transform.position = new Vector3(transform.position.x - transform.Find("bulletPosition").localPosition.x, transform.Find("bulletPosition").position.y, 0);
            b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
            blink.transform.position = new Vector3(transform.position.x - transform.Find("bulletPosition").localPosition.x, transform.Find("bulletPosition").position.y, 0);
        }
        shootCounter = 0;
    }
    void Shoot2() {
        if (energy > 0) {
            audioManager.PlaySound("shot2");
            energy--;
            ani.SetTrigger("Shoot2");
            GameObject b = Instantiate(bullet2);
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.6f, 0.20f);
            GameObject blink = Instantiate(bigblink2);
            if (sr.flipX) {
                b.transform.position = new Vector3(transform.Find("bulletPosition").localPosition.x * 1.1f + transform.position.x, transform.Find("bulletPosition").position.y, 0);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * 1.2f, 0);
                blink.transform.position = new Vector3(transform.Find("bulletPosition").localPosition.x + transform.position.x, transform.Find("bulletPosition").position.y, 0);
            } else {
                b.transform.position = new Vector3(transform.position.x - transform.Find("bulletPosition").localPosition.x * 1.1f, transform.Find("bulletPosition").position.y, 0);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed * 1.2f, 0);
                blink.transform.position = new Vector3(transform.position.x - transform.Find("bulletPosition").localPosition.x, transform.Find("bulletPosition").position.y, 0);
            }
        }
        shootCounter = 0;
    }
    void Flip(float n) {
        if(n < 0) {
            sr.flipX = false;
        }
        if(n > 0) {
            sr.flipX = true;
        }
    }
    public void IncreaseScore(int n) {
        score += n*scoreMultiplier;
    }
    public void TakeDamage(int n) {

        if(hasControl && hp - n >= 0 && !Dashing) {
            hp -= n;
            hc.SetBar(hp);
        }
        if(hp <= 0) {
            death();
        }

    }
    public void Cure(int n) {
        hp = Mathf.Min(hp + n, MaxHp);
        hc.SetBar(hp);
    }
    void Dash() {
        if (energy > 0) {
            audioManager.PlaySound("dash");
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
                DashDirection = 270;
            }
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0) {
                DashDirection = 315;
            }
        }

    }
    public void knockBack(float speed, Transform col) {
        float dif_x = col.position.x - transform.position.x;
        float dif_y = col.position.y - transform.position.y;

        float ang = Mathf.Atan2(-dif_y, -dif_x);

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(ang), speed * Mathf.Sin(ang));
        OffControl(0.5f);

    }

    public void death() {
        audioManager.PlaySound("death");
        audioManager.StopSound("miasma");
        PlayerPrefs.SetInt("score", score);
        SceneManager.LoadScene("SaveScore", LoadSceneMode.Single);
    }
}
