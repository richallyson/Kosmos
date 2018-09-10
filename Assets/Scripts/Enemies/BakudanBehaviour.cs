using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakudanBehaviour : MonoBehaviour {

    public float attackSpeed;

    public int damage;

    public float waitTime;
    private float waitTimeTimer;
    private bool canWait = false;

    public float explosionRadius = 3;
    public GameObject explosion;

    public float borderDist = 5;

    private bool invested = false;


    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Kosmos");
        if (player == null)
        {
            Debug.Log("Kosmos não foi encontrado");
        }
        GetComponent<EnemyLifeController>().name = "bakudan";
    }
	
	// Update is called once per frame
	void Update () {
		if (canWait) {
            if (Time.time - waitTimeTimer >= waitTime) {
                investidaTrovao();
            }
        } else {
            moveToPosition();
        }
	}

    //move o bakudan para a posição em x que ele vai ficar parado
    //a posição em y é a que ele nasceu
    private void moveToPosition() {
        Vector2 tmp = new Vector2(transform.position.x, transform.position.y);

        tmp.x -= 2 * Time.deltaTime;

        transform.position = tmp;

        if(transform.position.x <= borderDist) {
            canWait = true;
            waitTimeTimer = Time.time;
        }
    }

    //o nome é uma brincadeirinha
    private void investidaTrovao() {
        if (invested)
            return;
        Transform col = player.transform;
        float dif_x = col.position.x - transform.position.x;
        float dif_y = col.position.y - transform.position.y;

        float ang = Mathf.Atan2(dif_y, dif_x);

        GetComponent<Rigidbody2D>().velocity = new Vector2(attackSpeed * Mathf.Cos(ang), attackSpeed * Mathf.Sin(ang));
        invested = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            explode();
            GetComponent<EnemyLifeController>().Death();
        }
    }

    private void explode() {
        GameObject.Find("Main Camera").GetComponent<ShakeCamera>().Shake(0.04f, 0.04f);
        GameObject b = Instantiate(explosion);
        b.GetComponent<BakudanExplosion>().damage = damage;
        b.transform.position = transform.position;
        
    }
}
