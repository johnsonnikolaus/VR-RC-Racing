using UnityEngine;
using System.Collections;

public class CameraPositioner : MonoBehaviour {

    public GameObject[] players;
    public Vector3 middlePoint;
    public float distanceBetween;

    public bool trackEditor;

	// Use this for initialization
	void Start () {
        
        if(!trackEditor)
            players = GameObject.FindGameObjectsWithTag("Player");
	
	}
	
	// Update is called once per frame
	void Update () {

        if (trackEditor) 
        {
            AxisArrowScript axis = GameObject.Find("AxisArrows").GetComponent<AxisArrowScript>();
            try
            {
                if (axis.selectedTrackPiece == null)
                {
                    players = GameObject.FindGameObjectsWithTag("Player");
                    distanceBetween = Vector3.Distance(players[0].transform.position, players[1].transform.position);
                    middlePoint = ((players[0].transform.position / 2) + (players[1].transform.position / 2));
                    transform.position = middlePoint;
                }
            }
            catch
            {
                Debug.Log("No piece selected, but cars not placed");
            }
        }

        if (!trackEditor)
        {
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
}
