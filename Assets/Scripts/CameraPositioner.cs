using UnityEngine;
using System.Collections;

public class CameraPositioner : MonoBehaviour {

    public GameObject[] players;
    public Vector3 middlePoint;
    public float distanceBetween;

	// Use this for initialization
	void Start () {
        
        players = GameObject.FindGameObjectsWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {

        if (players.Length > 1)
        {
            distanceBetween = Vector3.Distance(players[0].transform.position, players[1].transform.position);
            middlePoint = ((players[0].transform.position / 2) + (players[1].transform.position / 2));
            transform.position = middlePoint;
        }
        else
            transform.position = players[0].transform.position;
	
	}
}
