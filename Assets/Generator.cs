using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public GameObject obj;
    public float delay;
    private float lastGen;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float minX = transform.position.x - transform.localScale.x / 2;
        float maxX = transform.position.x + transform.localScale.x / 2;
        float minY = transform.position.y - transform.localScale.y / 2;
        float maxY = transform.position.y + transform.localScale.y / 2;
        if (Time.time - lastGen > delay) {
            GameObject o = Instantiate(obj);
            o.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);
            lastGen = Time.time;
        }
	}
}
