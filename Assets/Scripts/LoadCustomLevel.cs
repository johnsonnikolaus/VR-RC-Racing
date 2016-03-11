using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

public class LoadCustomLevel : MonoBehaviour {

    public GameObject[] trackPieces;
    public string gamePath;
    public string levelToLoad;
    public string currentLine;
    public Text messageText;

    void Start()
    {
        messageText = GameObject.Find("Message Text").GetComponent<Text>();
    }

    void Update()
    {
        levelToLoad = GameObject.Find("InputField (1)").GetComponentInChildren<Text>().text;
        gamePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\VR RC Racing\Levels\" + levelToLoad + ".txt";
    }

    public void LoadLevel()
    {
        try
        {
            GameObject[] existingPieces = GameObject.FindGameObjectsWithTag("TrackPiece");
            for (int i = 0; i < existingPieces.Length; i++)
            {
                Destroy(existingPieces[i].gameObject);
            }
        }
        catch
        {
            Debug.Log("No existing track pieces.");
        }
        try
        {
            StreamReader sr = new StreamReader(gamePath);
            int lineNumber = File.ReadAllLines(gamePath).Length;
            while ((currentLine = sr.ReadLine()) != null)
            {
                if (currentLine.StartsWith("*"))
                {
                    try
                    {

                        GameObject newTrackPiece = Instantiate(trackPieces[System.Convert.ToUInt16(currentLine.Substring(currentLine.IndexOf("ID<") + 3, 1))], Vector3.zero, Quaternion.identity) as GameObject;
                        SetPosition(newTrackPiece.transform.GetChild(0));
                        SetRotation(newTrackPiece.transform.GetChild(0));
                        StartCoroutine(ShowMessage("Track loaded successfully."));
                    }
                    catch
                    {
                        StartCoroutine(ShowMessage("Unable to load track. File Corrupted/Modified syntax."));
                    }
                }
                else
                {
                    StartCoroutine(ShowMessage("Unable to load track. File Corrupted/Modified syntax."));
                    break;
                }
            }
        }
        catch
        {
            StartCoroutine(ShowMessage("Track doesn't exist. Make sure the track name is correct."));
        }
    }

    void SetPosition(Transform trackPiece)
    {
        int i1;
        int i2;
        int i3;

        int xPos = 0;
        int yPos = 0;
        int zPos = 0;

        //X Position
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posX<") + 5, 1), out i1))
        {
            xPos = i1;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posX<") + 5, 2), out i2))
        {
            xPos = i2;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posX<") + 5, 3), out i3))
        {
            xPos = i3;
        }

        //Y Position
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posY<") + 5, 1), out i1))
        {
            yPos = i1;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posY<") + 5, 2), out i2))
        {
            yPos = i2;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posY<") + 5, 3), out i3))
        {
            yPos = i3;
        }

        //Z Position
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posZ<") + 5, 1), out i1))
        {
            zPos = i1;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posZ<") + 5, 2), out i2))
        {
            zPos = i2;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("posZ<") + 5, 3), out i3))
        {
            zPos = i3;
        }

        trackPiece.transform.position = new Vector3(xPos, yPos, zPos);
    }

    void SetRotation(Transform trackPiece)
    {
        int i1;
        int i2;
        int i3;

        float xRot = 0;
        float yRot = 0;
        float zRot = 0;

        //X Rotation
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotX<") + 5, 1), out i1))
        {
            xRot = i1;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotX<") + 5, 2), out i2))
        {
            xRot = i2;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotX<") + 5, 3), out i3))
        {
            xRot = i3;
        }

        //Y Rotation
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotY<") + 5, 1), out i1))
        {
            yRot = i1;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotY<") + 5, 2), out i2))
        {
            yRot = i2;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotY<") + 5, 3), out i3))
        {
            yRot = i3;
        }

        //Z Rotation
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotZ<") + 5, 1), out i1))
        {
            zRot = i1;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotZ<") + 5, 2), out i2))
        {
            zRot = i2;
        }
        if (int.TryParse(currentLine.Substring(currentLine.IndexOf("rotZ<") + 5, 3), out i3))
        {
            zRot = i3;
        }

        trackPiece.transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    IEnumerator ShowMessage(string message)
    {
        messageText.text = message;
        messageText.enabled = true;
        yield return new WaitForSeconds(3);
        messageText.enabled = false;
    }

}
