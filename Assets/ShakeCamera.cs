using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {
    private float duration;
    private float radius;
    private bool shaking;
    private Vector2 originalPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (shaking) {
            transform.position = new Vector3(originalPosition.x + Random.Range(-radius, radius), originalPosition.x + Random.Range(-radius, radius), transform.position.z);
            duration -= Time.deltaTime;
            if(duration <= 0) {
                shaking = false;
                transform.position = new Vector3(0, 0, transform.position.z);
            }
        }
	}
    public void Shake(float durationP, float radiusP) {
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        shaking = true;
        duration = durationP;
        radius = radiusP;
    }
}
