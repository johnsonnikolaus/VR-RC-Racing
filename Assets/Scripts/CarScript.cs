using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {

    private Rigidbody rigid;
    private GameManager gameManager;

    public Ray groundDetection;

    [Header("Driving Variables")]
    public float forwardForce;
    public float turnForce;
    [Space()]
    [Header("Active Variables")]
    public Vector3 checkpointPos;
    public Quaternion checkpointRot;
    public bool canControl;
    public bool isGrounded;
    [Space()]
    [Header("Controller Variables")]
    public bool isKeyboard;

	// Use this for initialization
	void Start () {

        rigid = transform.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);

        if (isGrounded)
            canControl = true;
        else
        {
            rigid.AddForce(Vector3.down * 50);
            StartCoroutine(TimedAirControl());
        }

        if (!canControl)
            StartCoroutine(TimedRespawn());

        groundDetection = new Ray(transform.position, -transform.right);

        if (Physics.Raycast(groundDetection, 2f))
            isGrounded = true;
        else
            isGrounded = false;

        Debug.DrawRay(transform.position, -transform.forward, Color.green);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, transform.right, Color.blue);

        if (canControl)
        {
            if (Input.GetAxisRaw("Trigger") != 0 || Input.GetAxisRaw("VerticalKey") != 0)
            {
                rigid.AddForce(transform.up * Input.GetAxisRaw("Trigger") * forwardForce * 10);
                if (isKeyboard)
                    rigid.AddForce(transform.up * Input.GetAxisRaw("VerticalKey") * -forwardForce * 10);

                if (Input.GetAxisRaw("Horizontal") != 0 && isGrounded)
                {
                    if (Mathf.RoundToInt(Input.GetAxisRaw("Trigger")) > 0 || Input.GetAxisRaw("VerticalKey") < 0)
                        rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("Horizontal") * turnForce * 10, (transform.position + -transform.up * 3));
                    if (Mathf.RoundToInt(Input.GetAxisRaw("Trigger")) < 0 || Input.GetAxisRaw("VerticalKey") > 0)
                        rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("Horizontal") * turnForce * -10, (transform.position + -transform.up * 3));
                }
            }
        }

        if (Input.GetButtonDown("Jump"))
            Respawn();

	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "RaceGate")
            coll.GetComponent<RaceGate>().UpdateLap();
        if (coll.transform.tag == "Checkpoint")
            Debug.Log("Checkpoint");
    }

    void Respawn()
    {
        transform.position = checkpointPos + new Vector3(0, .25f, 0);
        transform.rotation = checkpointRot;
    }

    IEnumerator TimedAirControl()
    {
        //Use a loop to check every second if the player has started touching the ground
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(1);
            if (isGrounded)
                break;
            else
                continue;
        }

        if (!isGrounded)
                canControl = false;
    }

    IEnumerator TimedRespawn()
    {
        yield return new WaitForSeconds(3);
        if (!canControl)
        {
            Respawn();
            yield return new WaitForSeconds(2);
        }
    }

}
