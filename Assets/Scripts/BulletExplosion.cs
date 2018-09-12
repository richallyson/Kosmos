using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour {
    public GameObject blueExplosion;
    public int damage;
    private AudioManager audioManager;
    // Use this for initialization
    void Start() {
        GameObject exp = Instantiate(blueExplosion);
        exp.transform.position = transform.position;
        audioManager = AudioManager.instance;
        audioManager.PlaySound("bakudan explosion");
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Alien") {
            collision.gameObject.GetComponent<EnemyLifeController>().damage(damage);
            GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.15f, 0.2f);
        }
    }
}
