using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject target;

    public Vector3 offset;

    private Vector3 vel;

	// Use this for initialization
	void Start () {

        vel = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + offset, ref vel, 2);
        transform.LookAt(target.transform.position);
	
	}
}
