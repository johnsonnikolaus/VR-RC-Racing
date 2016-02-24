using UnityEngine;
using System.Collections;

public class RaceGate : MonoBehaviour {

    private GameManager gameManager;

    public bool Checkpoint;
    public bool StartFinish;

	// Use this for initialization
	void Start () {

        if (StartFinish)
            transform.tag = "RaceGate";
        if (Checkpoint)
            transform.tag = "Checkpoint";

        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
	
	}

    public void OnTriggerEnter(Collider coll)
    {
        CarScript carScript = coll.GetComponent<CarScript>();
        carScript.lastYRot = coll.transform.rotation.y;
        carScript.checkpointPos = transform.position - new Vector3(0,2,0);
        carScript.checkpointRot = coll.transform.rotation;
    }

}
