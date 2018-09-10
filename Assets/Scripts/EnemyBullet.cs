using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public GameObject Explosion;
    private SpriteRenderer sr;
    private CircleCollider2D cc;
    public bool useEffect;
    public float delay;
    private ParticleSystem ps;
    private Player player;

    public float timerDeath;
    private float timer;
    private bool dead = false;
    // Use this for initialization
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        ps = GetComponent<ParticleSystem>();
        timer = 0;
    }

    // Update is called once per frame
    void Update() {
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
        sr.enabled = false;
        cc.enabled = false;
        ps.Stop();
        Destroy(gameObject, delay);
        dead = true;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Alien" && gameObject.tag == "Bullet") {
            collision.gameObject.GetComponent<EnemyLifeController>().damage(1);
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.15f, 0.2f);
            //está sendo implementado no inimigo
            // player.IncreaseScore(collision.gameObject.GetComponent<Alien>().value);
        }

        if (collision.gameObject.tag == "Player" && gameObject.tag == "EnemyBullet") {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.2f, 0.3f);
        }
        if (collision.gameObject.tag == "Bullet") {
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.1f);
            player.IncreaseScore(10);
        }
        AutoDestroy();
    }
}
