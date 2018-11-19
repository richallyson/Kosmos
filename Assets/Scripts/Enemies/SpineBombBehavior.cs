using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineBombBehavior : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    public int damage;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            GetComponent<ParticleSystem>().Stop();
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.04f);
            GameObject exp = Instantiate(explosion);
            exp.transform.position = transform.position;
            GetComponent<EnemyLifeController>().Death();
        }
    }
}
