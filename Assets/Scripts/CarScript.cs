using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {

    private Rigidbody rigid;

    public float forwardForce;
    public float turnForce;

    public Ray groundDetection;
    public Ray rightSideDetection;
    public Ray leftSideDetection;
    public Ray topSideDetection;

    public bool isGrounded;

    public bool isKeyboard;

	// Use this for initialization
	void Start () {

        rigid = transform.GetComponent<Rigidbody>();

        rightSideDetection = new Ray(transform.position, -transform.forward);
        leftSideDetection = new Ray(transform.position, transform.forward);
        topSideDetection = new Ray(transform.position, transform.right);
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);

        //isTipped();

        groundDetection = new Ray(transform.position, -transform.right);

        if (Physics.Raycast(groundDetection, 1))
            isGrounded = true;
        else
            isGrounded = false;

        Debug.DrawRay(transform.position, -transform.forward, Color.green);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, transform.right, Color.blue);


        if (Input.GetAxisRaw("Trigger") != 0 || Input.GetAxisRaw("VerticalKey") != 0)
        {
            rigid.AddForce(transform.up * Input.GetAxisRaw("Trigger") * forwardForce * 10);
            if(isKeyboard)
                rigid.AddForce(transform.up * Input.GetAxisRaw("VerticalKey") * -forwardForce * 10);

            if (Input.GetAxisRaw("Horizontal") != 0 && isGrounded)
            {
                if (Mathf.RoundToInt(Input.GetAxisRaw("Trigger")) > 0 || Input.GetAxisRaw("Vertical") < 0)
                rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("Horizontal") * turnForce * 10, (transform.position + -transform.up * 3));
                else
                    rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("Horizontal") * -turnForce * 10, (transform.position + -transform.up * 3));
            }

        }

        if (Input.GetButtonDown("Jump"))
        {
            transform.position = transform.position + new Vector3(0, 3, 0);
            transform.rotation = Quaternion.Euler(0, 180, 90);
        }

        if(!isGrounded)
            rigid.AddForce(Vector3.down * 50);

	}

    /*

    bool isTipped()
    {
        //Left Side Detection
        if (Physics.Raycast(leftSideDetection, 1))
        {
            StartCoroutine(TipTimer(2));
            Debug.Log("LeftSide");
            return true;
        }
        else
            return false;

        //Right Side Detection
        if (Physics.Raycast(rightSideDetection, 1))
        {
            StartCoroutine(TipTimer(2));
            Debug.Log("RightSide");
            return true;
        }
        else
            return false;

        //Top Side Detection
        if (Physics.Raycast(topSideDetection, 1))
        {
            StartCoroutine(TipTimer(2));
            Debug.Log("TopSide");
            return true;
        }
        else
            return false;
    }

    IEnumerator TipTimer(int time)
    {
        yield return new WaitForSeconds(time);
        if (isTipped())
        {
            transform.position = transform.position + new Vector3(0, 3, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    
     */

}
