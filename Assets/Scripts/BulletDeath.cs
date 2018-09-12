using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeath : MonoBehaviour {
    public GameObject Explosion;
    private SpriteRenderer sr;
    private CircleCollider2D cc;
    public bool useEffect;
    public float delay;
    private ParticleSystem ps;
    private Rigidbody2D rb;
    private Player player;

    public float timerDeath;
    private float timer;
    private bool dead = false;
    public int Damage = 1;
    public bool useFieldDamage;
    public GameObject fieldDamage;
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        ps = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Kosmos").GetComponent<Player>();
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!dead) {
            timer += Time.deltaTime;
            if (timer >= timerDeath) {
                AutoDestroy();
            }
        }
    }
    public void AutoDestroy() {
        if (useEffect) {
            GameObject exp = Instantiate(Explosion);
            exp.transform.position = transform.position;
        }
        if (useFieldDamage) {
            GameObject fd = Instantiate(fieldDamage);
            fd.transform.position = transform.position;
        }
        sr.enabled = false;
        cc.enabled = false;
        ps.Stop();
        Destroy(gameObject, delay);
        dead = true;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Alien" && gameObject.tag == "Bullet") {
            collision.gameObject.GetComponent<EnemyLifeController>().damage(Damage);
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.15f, 0.2f);
            //está sendo implementado no inimigo
            // player.IncreaseScore(collision.gameObject.GetComponent<Alien>().value);
            if (rb.mass < 20) {
                audioManager.PlaySound("shoot alien");
            } else {
                audioManager.PlaySound("shoot 2 alien");
            }
            AutoDestroy();
        }

        if(collision.gameObject.tag == "Player" && gameObject.tag == "EnemyBullet") {
            audioManager.PlaySound("alien1 shoot col");
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.2f, 0.3f);
            AutoDestroy();
        }
        if (collision.gameObject.tag == "Bullet" && gameObject.tag == "EnemyBullet") {
            audioManager.PlaySound("alien1 shoot col");
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.05f, 0.05f);
            AutoDestroy();
        }
        if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Bullet") {
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.1f);
            player.IncreaseScore(10);
            AutoDestroy();
        }
        if (collision.gameObject.tag == "EnemyBullet" && gameObject.tag == "Bullet") {
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.08f);
            player.IncreaseScore(5);
            
        }
        if (collision.gameObject.tag == "Objeto" && gameObject.tag == "Bullet") {
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.08f);
            player.IncreaseScore(10);
            AutoDestroy();
        }

    }
}
