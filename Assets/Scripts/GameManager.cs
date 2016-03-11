using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    static public int numOfPlayers = 1;

    [SerializeField]
    static public string p1Color = "red";
    [SerializeField]
    static public string p2Color = "blue";

    private Text lapText;
    private Text winnerText;
    private Canvas gameOverCanvas;
    private Canvas pauseCanvas;
    private Canvas hudCanvas;

    public Material redMat;
    public Material blueMat;
    public Material yellowMat;
    public Material greenMat;

    public GameObject[] players;

    public int lapsToFinish;
    public int winningPlayer;

	// Use this for initialization
	void Start () {

        winningPlayer = 0;
        Time.timeScale = 1;
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > numOfPlayers)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<CarScript>().player > numOfPlayers)
                    if (players[i].transform.name != ("RC Car 1"))
                    {
                        Destroy(players[i].gameObject);
                    }
                else
                    continue;
            }
        }

        lapText = GameObject.Find("Lap Text").GetComponent<Text>();
        winnerText = GameObject.Find("Winner Text").GetComponent<Text>();
        gameOverCanvas = GameObject.Find("Game Over Canvas").GetComponent<Canvas>();
        pauseCanvas = GameObject.Find("Pause Canvas").GetComponent<Canvas>();
        hudCanvas = GameObject.Find("HUD").GetComponent<Canvas>();

        pauseCanvas.enabled = false;
        gameOverCanvas.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0 && winningPlayer == 0)
            {
                Time.timeScale = 0;
                pauseCanvas.enabled = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseCanvas.enabled = false;
            }
            
        }

        if (winningPlayer != 0)
        {
            //Time.timeScale = 0;
            gameOverCanvas.enabled = true;
            hudCanvas.enabled = false;
            winnerText.text = "Player " + winningPlayer + " Won!";
        }
        else
        {
            gameOverCanvas.enabled = false;
            hudCanvas.enabled = true;
        }

        if (numOfPlayers == 1)
            lapText.text = "Player " + GameObject.Find("RC Car 1").GetComponent<CarScript>().player + ": " + GameObject.Find("RC Car 1").GetComponent<CarScript>().lap + "/" + lapsToFinish;

        if(numOfPlayers == 2)
            lapText.text = "Player " + players[0].GetComponent<CarScript>().player + ": " + players[0].GetComponent<CarScript>().lap + "/" + lapsToFinish + "\n" +
                "Player " + players[1].GetComponent<CarScript>().player + ": " + players[1].GetComponent<CarScript>().lap + "/" + lapsToFinish + "\n";
	
	}
}
