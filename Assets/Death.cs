using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
    public GameObject Explosion;
    private SpriteRenderer sr;
    private CircleCollider2D cc;
    public bool useEffect;
    public float delay;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision) {
        if (useEffect) {
            GameObject exp = Instantiate(Explosion);
            exp.transform.position = transform.position;
        }
        sr.enabled = false;
        cc.enabled = false;
        Destroy(gameObject, delay);
    }
}
