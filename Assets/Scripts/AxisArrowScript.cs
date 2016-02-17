using UnityEngine;
using System.Collections;

public class AxisArrowScript : MonoBehaviour
{

    public GameObject selectedAxis;
    public GameObject selectedTrackPiece;

    public GameObject xArrow;
    public GameObject yArrow;
    public GameObject zArrow;
    public GameObject rotArrow;

    public CameraFollow camFollow;

    private Quaternion startingRotation;


    // Use this for initialization
    void Start()
    {

        startingRotation = transform.rotation;
        camFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();

    }

    // Update is called once per frame
    void Update()
    {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(0))
        {

            selectedAxis = hit.transform.gameObject;

            if (selectedAxis == xArrow && selectedTrackPiece != null)
            {
                transform.Translate(Vector3.right * 8);
                selectedTrackPiece.transform.Translate(Vector3.right * 8);
            }

            if (selectedAxis == yArrow && selectedTrackPiece != null)
            {
                transform.Translate(Vector3.up * 4);
                selectedTrackPiece.transform.Translate(transform.forward * 4);
            }

            if (selectedAxis == zArrow && selectedTrackPiece != null)
            {
                transform.Translate(Vector3.forward * 5);
                selectedTrackPiece.transform.Translate(transform.up * -5);
            }

            if (selectedAxis == rotArrow && selectedTrackPiece != null)
            {
                transform.Rotate(transform.up * -90);
                selectedTrackPiece.transform.Rotate(Vector3.forward * -90);
            }

            if (hit.transform.tag == "TrackPiece")
            {
                transform.position = hit.transform.position + transform.up;
                transform.rotation = startingRotation;
                selectedTrackPiece = hit.transform.parent.gameObject;
                selectedTrackPiece.transform.rotation = Quaternion.Euler(0, 0, 0);
                camFollow.target = selectedTrackPiece;
            }

        }

        if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(1))
        {
            if (selectedAxis == yArrow && selectedTrackPiece != null)
            {
                transform.Translate(Vector3.up * -4);
                selectedTrackPiece.transform.Translate(transform.forward * -4);
            }

            if (!Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(0))
            {
                selectedTrackPiece = null;
            }

            if (Input.GetMouseButtonUp(0))
                selectedAxis = null;

        }

    }

}
