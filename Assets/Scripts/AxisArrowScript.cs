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

    public GameObject camPos;

    private Quaternion startingRotation;


    // Use this for initialization
    void Start()
    {

        startingRotation = transform.rotation;
        camPos = GameObject.Find("CameraPositioner");

    }

    // Update is called once per frame
    void Update()
    {


        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        #region MouseControls

        if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(0))
        {

            selectedAxis = hit.transform.gameObject;

            if (selectedAxis == xArrow && selectedTrackPiece != null)
            {
                transform.Translate(transform.right * 2, Space.World);
                selectedTrackPiece.transform.Translate(transform.right * 2, Space.World);
            }

            if (selectedAxis == yArrow && selectedTrackPiece != null)
            {
                transform.Translate(transform.up * 1, Space.World);
                selectedTrackPiece.transform.Translate(transform.up * 1, Space.World);
            }

            if (selectedAxis == zArrow && selectedTrackPiece != null)
            {
                transform.Translate(transform.forward * 2, Space.World);
                selectedTrackPiece.transform.Translate(transform.forward * 2, Space.World);
            }

            if (selectedAxis == rotArrow && selectedTrackPiece != null)
            {
                transform.Rotate(transform.up * -90, Space.World);
                selectedTrackPiece.transform.Rotate(transform.up * -90, Space.World);
            }

            camPos.transform.position = selectedTrackPiece.transform.position;

        }

        if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonUp(0))
        {
            if (hit.transform.tag == "TrackPiece")
            {
                transform.position = hit.transform.position + transform.up * 5;
                transform.rotation = hit.transform.parent.rotation;
                selectedTrackPiece = hit.transform.parent.gameObject;
                camPos.transform.position = selectedTrackPiece.transform.position;
            }
        }

        if (Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonDown(1))
        {

            if (hit.transform.tag == "TrackPiece")
                Destroy(hit.transform.parent.gameObject);

            selectedAxis = hit.transform.gameObject;

            if (selectedAxis == xArrow && selectedTrackPiece != null)
            {
                transform.Translate(transform.right * -2, Space.World);
                selectedTrackPiece.transform.Translate(transform.right * -2, Space.World);
            }

            if (selectedAxis == yArrow && selectedTrackPiece != null)
            {
                transform.Translate(transform.up * -1, Space.World);
                selectedTrackPiece.transform.Translate(transform.up * -1, Space.World);
            }

            if (selectedAxis == zArrow && selectedTrackPiece != null)
            {
                transform.Translate(transform.forward * -2, Space.World);
                selectedTrackPiece.transform.Translate(transform.forward * -2, Space.World);
            }

            if (selectedAxis == rotArrow && selectedTrackPiece != null)
            {
                transform.Rotate(transform.up * 90, Space.World);
                selectedTrackPiece.transform.Rotate(transform.up * 90, Space.World);
            }

            camPos.transform.position = selectedTrackPiece.transform.position;

        }

        if (!Physics.Raycast(mouseRay, out hit) && Input.GetMouseButtonUp(0))
            selectedTrackPiece = null;

        #endregion

        #region KeyboardControls

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(transform.forward * 2, Space.World);
            selectedTrackPiece.transform.Translate(transform.forward * 2, Space.World);
            camPos.transform.position = selectedTrackPiece.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(transform.forward * -2, Space.World);
            selectedTrackPiece.transform.Translate(transform.forward * -2, Space.World);
            camPos.transform.position = selectedTrackPiece.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(transform.right * -2, Space.World);
            selectedTrackPiece.transform.Translate(transform.right * -2, Space.World);
            camPos.transform.position = selectedTrackPiece.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(transform.right *2, Space.World);
            selectedTrackPiece.transform.Translate(transform.right * 2, Space.World);
            camPos.transform.position = selectedTrackPiece.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedTrackPiece.transform.Rotate(transform.up * -90, Space.World);
            camPos.transform.position = selectedTrackPiece.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedTrackPiece.transform.Rotate(transform.up * 90, Space.World);
            camPos.transform.position = selectedTrackPiece.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.Translate(transform.up * 1, Space.World);
            selectedTrackPiece.transform.Translate(transform.up * 1, Space.World);
            camPos.transform.position = selectedTrackPiece.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.Translate(transform.up * -1, Space.World);
            selectedTrackPiece.transform.Translate(transform.up * -1, Space.World);
            camPos.transform.position = selectedTrackPiece.transform.position;
        }
        #endregion

    }

}
