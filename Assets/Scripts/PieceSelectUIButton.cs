using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class PieceSelectUIButton : MonoBehaviour {

    public GameObject[] players = new GameObject[2];
    public AxisArrowScript axis;

	// Use this for initialization
	void Start () {
        axis = GameObject.Find("AxisArrows").GetComponent<AxisArrowScript>();
	}
	

    public void AddPiece(GameObject piece)
    {
        if(axis.selectedTrackPiece != null)
            Instantiate(piece, axis.selectedTrackPiece.transform.position, Quaternion.identity);
        else
            Instantiate(piece, Vector3.zero, Quaternion.identity);
    }

    public void PlacePlayers()
    {
        Instantiate(players[0], Vector3.zero + transform.forward * 2 + transform.up * 2, Quaternion.identity);
        Instantiate(players[1], Vector3.zero + -transform.forward * 2 + transform.up * 2, Quaternion.identity);
    }

    public void SaveGame()
    {
        string levelName = GameObject.Find("InputField").GetComponentInChildren<Text>().text;
        string gamePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        if (!Directory.Exists(gamePath + @"\VR RC Racing\"))
            Directory.CreateDirectory(gamePath + @"\VR RC Racing\");
        gamePath = gamePath + @"\VR RC Racing\";
        if (!Directory.Exists(gamePath + @"\Levels\"))
        {
            Directory.CreateDirectory(gamePath + @"\Levels\");
            gamePath = gamePath + @"\Levels\";
        }
        else
            gamePath = gamePath + @"\Levels\";
        gamePath = gamePath + levelName + ".txt";
        File.Create(gamePath).Dispose();
        GameObject[] trackPieces = GameObject.FindGameObjectsWithTag("TrackPiece");
        for (int i = 0; i < trackPieces.Length; i++)
        {
            if (trackPieces[i].transform.parent != null)
                continue;
            int pieceID;
            switch (trackPieces[i].transform.name)
            {
                case "Straight Piece(Clone)":
                    pieceID = 0;
                    break;
                case "Small Ramp Straight(Clone)":
                    pieceID = 1;
                    break;
                case "Right Curve Piece(Clone)":
                    pieceID = 2;
                    break;
                case "Left Curve Piece(Clone)":
                    pieceID = 3;
                    break;
                case "Open Piece(Clone)":
                    pieceID = 4;
                    break;
                case "Cylinder Obstacle(Clone)":
                    pieceID = 5;
                    break;
                case "Small Jump(Clone)":
                    pieceID = 6;
                    break;
                case "Racing Gate(Clone)":
                    pieceID = 7;
                    break;
                default:
                    File.AppendText("Error creating level: Invalid track piece");
                    return;
            }
            File.AppendAllText(gamePath, "*");
            File.AppendAllText(gamePath, "ID<" + pieceID + ">");
            File.AppendAllText(gamePath, "posX<" + trackPieces[i].transform.GetChild(0).transform.position.x + ">endPosX");
            File.AppendAllText(gamePath, "posY<" + trackPieces[i].transform.GetChild(0).transform.position.y + ">endPosY");
            File.AppendAllText(gamePath, "posZ<" + trackPieces[i].transform.GetChild(0).transform.position.z + ">endPosZ");
            File.AppendAllText(gamePath, "rotX<" + trackPieces[i].transform.GetChild(0).transform.rotation.eulerAngles.x + ">endRotX");
            File.AppendAllText(gamePath, "rotY<" + trackPieces[i].transform.GetChild(0).transform.rotation.eulerAngles.y + ">endRotY");
            File.AppendAllText(gamePath, "rotZ<" + trackPieces[i].transform.GetChild(0).transform.rotation.eulerAngles.z + ">endRotZ");
            File.AppendAllText(gamePath, System.Environment.NewLine);
        }
    }

}
