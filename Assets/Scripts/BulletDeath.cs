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
    private Player player;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        ps = GetComponent<ParticleSystem>();
        player = GameObject.Find("Kosmos").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Alien") {
            collision.gameObject.GetComponent<EnemyLifeController>().damage(1);
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.15f, 0.2f);
            //está sendo incrementado no inimigo
           // player.IncreaseScore(collision.gameObject.GetComponent<Alien>().value);
        }
        if (useEffect) {
            GameObject exp = Instantiate(Explosion);
            exp.transform.position = transform.position;
        }
        sr.enabled = false;
        cc.enabled = false;
        ps.Stop();
        Destroy(gameObject, delay);
    }
}
