using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatlingun : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject bullet;
    private Transform bulletExit;
    public float speed;
    public float bullet_speed;
    public float delay;
    private float lastShot;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        bulletExit = transform.Find("BulletExitShape");
        lastShot = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastShot > delay) {
            Shot();
            lastShot = Time.time;
        }
	}

    public void Shot() {
        GameObject blt = Instantiate(bullet);
        blt.transform.position = new Vector3(bulletExit.localPosition.x + transform.position.x, bulletExit.localPosition.y + transform.position.y + Random.Range(-0.8f, 0.8f));
        blt.GetComponent<Rigidbody2D>().velocity = new Vector2(-bullet_speed, 0);

    }
}
