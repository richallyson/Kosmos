using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeController : MonoBehaviour {

    //HP do inimigo
    public int hp;

    //Total de pontos que o inimigo da ao player
    public int points;

    private GameObject player;
    private SpriteRenderer sr;
    private EdgeCollider2D ec;
    private Rigidbody2D rb;
    private Color originalColor;

    public bool dying = false;
    public GameObject explosion;
    private float hue = 1.0f;
    public bool hasDeathAnimation = false;

    public GameObject lilImpact;
    public string mname;
    public float HPDropChance;
    public GameObject hpdrop;
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ec = GetComponent<EdgeCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Kosmos");
        if (player == null)
        {
            Debug.Log("Kosmos não foi encontrado");
        }
        originalColor = sr.color;
    }

    public void Update() {

        //animação de morte
        if (dying) {
            hue -= Time.deltaTime;
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, hue);
        }
    }

    public void damage(int amount)
    {
        hp -= amount;
        if (hp <= 0) {
            Death();
        }
    }

    public void Death()
    {
        GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.15f, 0.4f);
        //adciona os pontos ao player

        player.GetComponent<Player>().IncreaseScore(points);
        if (hasDeathAnimation) {
            if(Random.Range(0.0f, 1.0f) < HPDropChance) {
                GameObject hp = Instantiate(hpdrop);
                hp.transform.position = transform.position;
            }
            //animação de morte
            dying = true;
            rb.velocity = new Vector2(0, 0);
            if (transform.Find("Trail") != null) {
                transform.Find("Trail").GetComponent<ParticleSystem>().Stop();
            }
            ec.enabled = false;
            GameObject exp = Instantiate(explosion);
            exp.transform.position = transform.position;
            Destroy(gameObject, 1.0f);
        }
    }
    public void CleanDeath() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && mname != "bakudan")
        {
            
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
            //Death();
            knockBack(5, collision.transform);
            collision.gameObject.GetComponent<Player>().knockBack(5, transform);
            damage(1);
            GameObject lp = Instantiate(lilImpact);
            lp.transform.position = collision.GetContact(0).point;
            //falta dar o knocBack no player também
        }
        if(collision.gameObject.tag == "EnemyDeathBound") {
            CleanDeath();
        }
    }

    private void knockBack(float speed, Transform col)
    {
        float dif_x = col.position.x - transform.position.x;
        float dif_y = col.position.y - transform.position.y;

        float ang = Mathf.Atan2(-dif_y, -dif_x);

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(ang), speed * Mathf.Sin(ang));


    }
}
