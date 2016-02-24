using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Canvas[] Menus;
    public Material[] carMats;
    public GameObject previewCar;
    public bool isMainMenu;
    public Text playerNotifier;

    private int currentMenu;

    //Used to show which player you are selecting a color for
    private int currentColorSelection;

    void Start()
    {
        Time.timeScale = 1;
        currentColorSelection = 1;
        currentMenu = 0;
        if(isMainMenu)
            playerNotifier = GameObject.Find("Player Notifier").GetComponent<Text>();
    }

    void Update()
    {
        if (isMainMenu)
        {
            playerNotifier.text = "Select color for Car " + currentColorSelection;
            transform.LookAt(Menus[currentMenu].transform.position);
        }
    }

    public void PlayAgain()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadLevel(int level)
    {
        Application.LoadLevel(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetNumberOfPlayers(int amount)
    {
        GameManager.numOfPlayers = amount;
    }

    public void GoToMenu(int menu)
    {
        currentMenu = menu;
    }

    public void ColorSelectionNext()
    {
        currentColorSelection += 1;
        if (currentColorSelection > GameManager.numOfPlayers)
        {
            GoToMenu(3);
        }
    }

    public void SetColor(int value)
    {
        previewCar.GetComponent<MeshRenderer>().material = carMats[value];

        if (currentColorSelection == 1)
        {
            if (value == 0)
                GameManager.p1Color = "red";
            if (value == 1)
                GameManager.p1Color = "blue";
            if (value == 2)
                GameManager.p1Color = "yellow";
            if (value == 3)
                GameManager.p1Color = "green";
        }

        if (currentColorSelection == 2)
        {
            if (value == 0)
                GameManager.p2Color = "red";
            if (value == 1)
                GameManager.p2Color = "blue";
            if (value == 2)
                GameManager.p2Color = "yellow";
            if (value == 3)
                GameManager.p2Color = "green";
        }
    }
	
}
