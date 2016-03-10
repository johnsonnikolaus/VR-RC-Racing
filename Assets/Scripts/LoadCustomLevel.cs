using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class LoadCustomLevel : MonoBehaviour {

    public GameObject[] trackPieces;
    public string gamePath;
    public string levelToLoad;

    void Update()
    {
        levelToLoad = GameObject.Find("InputField (1)").GetComponentInChildren<Text>().text;
        gamePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\VR RC Racing\" + levelToLoad + ".txt";
    }

    public void LoadLevel()
    {
        
        StreamReader sr = new StreamReader(gamePath);
        string currentLine;
        while ((currentLine = sr.ReadLine()) != null)
        {
            if (currentLine.StartsWith("ID"))
            {

                GameObject newTrackPiece = Instantiate(trackPieces[System.Convert.ToInt16(currentLine.Substring(currentLine.IndexOf("<"),1))], Vector3.zero, Quaternion.identity) as GameObject;
            }
        }
    }

}
