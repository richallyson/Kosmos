using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakudanExplosion : MonoBehaviour {
    public GameObject redExplosion;
    public int damage;
	// Use this for initialization
	void Start () {
        GameObject exp = Instantiate(redExplosion);
        exp.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            collision.gameObject.GetComponent<Player>().knockBack(5, transform);
        }
    }
}
