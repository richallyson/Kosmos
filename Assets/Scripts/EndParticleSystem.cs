using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndParticleSystem : MonoBehaviour {
    private ParticleSystem ps;
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject, ps.main.startLifetime.constantMax + ps.main.duration);
	}
}
