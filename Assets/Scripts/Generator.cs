using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public GameObject obj;
    public float delay;
    private float lastGen;
    public float StartAt;
    private float timer;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    public bool RandomRotation;
    // Use this for initialization
    void Start () {
        minX = transform.position.x - transform.localScale.x / 2;
        maxX = transform.position.x + transform.localScale.x / 2;
        minY = transform.position.y - transform.localScale.y / 2;
        maxY = transform.position.y + transform.localScale.y / 2;
        
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > StartAt && Time.time - lastGen > delay) {
            GameObject o = Instantiate(obj);
            o.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);
            if (RandomRotation) {
                o.transform.Rotate(0, 0, Random.Range(0, 359));
                o.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-6.0f, 6.0f));
            }
            lastGen = Time.time;
        }
	}
}
