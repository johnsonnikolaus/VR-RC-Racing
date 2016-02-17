using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public CameraPositioner camPosScript;
    private int distance;

    public GameObject target;

    public Vector3 offset;

    private Vector3 vel;

	// Use this for initialization
	void Start () {

        vel = Vector3.zero;
        target = GameObject.Find("CameraPositioner");
        camPosScript = GameObject.Find("CameraPositioner").GetComponent<CameraPositioner>();
        offset = new Vector3(0, 15, -15);

	}
	
	// Update is called once per frame
	void Update () {

        distance = Mathf.RoundToInt(camPosScript.distanceBetween);

        offset = new Vector3(0, 15 * (camPosScript.distanceBetween / 40) + 10, -15 * (camPosScript.distanceBetween / 40) + -10);

        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + offset, ref vel, 1.5f);
        transform.LookAt(target.transform.position);
	
	}
}
