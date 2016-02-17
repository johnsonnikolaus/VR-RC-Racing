using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int lapsToFinish;
    public int winningPlayer;

	// Use this for initialization
	void Start () {

        winningPlayer = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(0);
	
	}
}
