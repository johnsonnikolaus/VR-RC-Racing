using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {

    private Rigidbody rigid;
    private GameManager gameManager;
    private AudioSource carAudio;

    public Ray groundDetection;
    [HideInInspector]
    public float startingWRot;
    [HideInInspector]
    public float lastYRot;

    private int respawnCount;

    public bool trackEditor;

    [Header("Driving Variables")]
    public float topSpeed;
    public float acceleration;
    public float turnForce;
    public float currentSpeed;
    [Space()]
    [Header("Audio Variables")]
    public float startingPitch = 1;
    public float pitchAdjust;
    [Space()]
    [Header("Paint Variables")]
    public bool paintMode;
    public Color paintColor;
    public GameObject paintMark;
    public Ray wheelRay;
    [Space()]
    [Header("Active Variables")]
    public int lap;
    public Vector3 checkpointPos;
    public Quaternion checkpointRot;
    public bool canControl;
    public bool isGrounded;
    public bool canActivateFinish;
    [Space()]
    [Header("Controller Variables")]
    public bool isKeyboard;
    public int player;

	// Use this for initialization
	void Start () {

        if (Application.loadedLevelName == "Track Editor")
            trackEditor = true;
        else
            trackEditor = false;
        startingWRot = transform.rotation.w;
        carAudio = transform.GetComponentInChildren<AudioSource>();
        canActivateFinish = true;
        rigid = transform.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        checkpointPos = transform.position;
        checkpointRot = transform.rotation;
        SetColorForPlayer();
        SetPaintColor();
	
	}
	
	// Update is called once per frame
	void Update () {

        //if (trackEditor)
        //{
        //    carAudio.volume = 0;
        //    canControl = false;
        //    return;
        //}

        Physics.IgnoreLayerCollision(2, 8, true);
        Physics.IgnoreLayerCollision(2, 9, false);

        Debug.DrawRay((transform.position - (-transform.up * 1.25f)), Vector3.up, Color.cyan);

        if (!canControl)
            StartCoroutine(TimedRespawn());

        groundDetection = new Ray(transform.position, -transform.right);

        if (Physics.Raycast(groundDetection, 2f))
        {
            isGrounded = true;
            pitchAdjust = 30;
        }
        else
        {
            isGrounded = false;
            pitchAdjust = 25;
        }

        if (player == 1)
        {
            if (Input.GetButtonDown("Respawn1"))
                Respawn();
            if (!isKeyboard)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Respawn();
                }
            }
        }

        if (player == 2)
        {
            if (Input.GetButtonDown("Respawn2"))
                Respawn();
            if (!isKeyboard)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Respawn();
                }
            }
        }

        carAudio.pitch = startingPitch + (currentSpeed / pitchAdjust);

        //Shows the directions of the car
        Debug.DrawRay(transform.position, -transform.forward, Color.green);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, transform.right, Color.blue);

    }

    void FixedUpdate () {

        if (isGrounded)
            canControl = true;
        else
        {
            rigid.AddForce(Vector3.down * 75);
            StartCoroutine(TimedAirControl());
        }

        if (canControl)
        {
            if (paintMode && currentSpeed != 0)
            {
                wheelRay = new Ray((transform.position - (-transform.up * 1.25f)), Vector3.down);
                if (Physics.Raycast(wheelRay, 0.7f))
                {
                    StartCoroutine(PaintTrack());
                }
            }

            if (isKeyboard)
            {
                if (player == 1)
                {
                    StartCoroutine(Accelerate());
                    rigid.AddForce(transform.up * Input.GetAxisRaw("VerticalKey1") * -currentSpeed * 10);
                    if (Input.GetAxisRaw("Horizontal1") != 0 && isGrounded)
                    {
                        //Set the respawn count to 0, so the player doesn't randomly respawn the wrong way
                        respawnCount = 0;
                        if (Input.GetAxisRaw("VerticalKey1") < 0)
                            rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("Horizontal1") * turnForce * 10, (transform.position + -transform.up * 3));
                        if (Input.GetAxisRaw("VerticalKey1") > 0)
                            rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("Horizontal1") * turnForce * -10, (transform.position + -transform.up * 3));
                    }
                    if (Input.GetAxisRaw("VerticalKey1") == 0)
                    {
                        StartCoroutine(Decelarate());
                    }
                }

                if (player == 2)
                {
                    StartCoroutine(Accelerate());
                    rigid.AddForce(transform.up * Input.GetAxisRaw("VerticalKey2") * -currentSpeed * 10);
                    if (Input.GetAxisRaw("Horizontal2") != 0 && isGrounded)
                    {
                        //Set the respawn count to 0, so the player doesn't randomly respawn the wrong way
                        respawnCount = 0;
                        if (Input.GetAxisRaw("VerticalKey2") < 0)
                            rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("Horizontal2") * turnForce * 10, (transform.position + -transform.up * 3));
                        if (Input.GetAxisRaw("VerticalKey2") > 0)
                            rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("Horizontal2") * turnForce * -10, (transform.position + -transform.up * 3));
                    }
                    if (Input.GetAxisRaw("VerticalKey2") == 0)
                    {
                        StartCoroutine(Decelarate());
                    }
                }
            }
            if(!isKeyboard)
            {
                Debug.Log(Mathf.RoundToInt(Input.GetAxisRaw("Trigger")));
                StartCoroutine(Accelerate());
                rigid.AddForce(transform.up * Input.GetAxisRaw("Trigger") * currentSpeed * 10);
                if (Mathf.RoundToInt(Input.GetAxisRaw("Trigger")) != 0)
                {
                    //Set the respawn count to 0, so the player doesn't randomly respawn the wrong way
                    respawnCount = 0;
                    if (Input.GetAxisRaw("JHorizontal") != 0 && isGrounded)
                    {
                        if (Input.GetAxisRaw("Trigger") > 0)
                            rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("JHorizontal") * turnForce * 10, (transform.position + -transform.up * 3));
                        if (Input.GetAxisRaw("Trigger") < 0)
                            rigid.AddForceAtPosition(transform.forward * Input.GetAxisRaw("JHorizontal") * turnForce * -10, (transform.position + -transform.up * 3));
                    }
                    if (Input.GetAxisRaw("Trigger") == 0)
                    {
                        StartCoroutine(Decelarate());
                    }

                }
            }
            
        }

	}

    void OnTriggerEnter(Collider coll)
    {
        //If the checkpoint is a race gate
        if (coll.transform.tag == "RaceGate" && canActivateFinish)
        {
            lap += 1;
            StartCoroutine(ActivationTimer());
            if (lap >= gameManager.lapsToFinish + 1)
            {
                gameManager.winningPlayer = player;
                canControl = false;
            }
        }
    }

    void SetColorForPlayer()
    {
        if (player == 1)
        {
            if (GameManager.p1Color == "red")
                transform.GetComponent<MeshRenderer>().material =  gameManager.redMat;
            if (GameManager.p1Color == "blue")
                transform.GetComponent<MeshRenderer>().material = gameManager.blueMat;
            if (GameManager.p1Color == "yellow")
                transform.GetComponent<MeshRenderer>().material = gameManager.yellowMat;
            if (GameManager.p1Color == "green")
                transform.GetComponent<MeshRenderer>().material = gameManager.greenMat;
        }

        if (player == 2)
        {
            if (GameManager.p2Color == "red")
                transform.GetComponent<MeshRenderer>().material = gameManager.redMat;
            if (GameManager.p2Color == "blue")
                transform.GetComponent<MeshRenderer>().material = gameManager.blueMat;
            if (GameManager.p2Color == "yellow")
                transform.GetComponent<MeshRenderer>().material = gameManager.yellowMat;
            if (GameManager.p2Color == "green")
                transform.GetComponent<MeshRenderer>().material = gameManager.greenMat;
        }
    }

    void SetPaintColor()
    {
        if (player == 1)
        {
            if (GameManager.p1Color == "red")
                paintColor = Color.red;
            if (GameManager.p1Color == "blue")
                paintColor = Color.blue;
            if (GameManager.p1Color == "yellow")
                paintColor = Color.yellow;
            if (GameManager.p1Color == "green")
                paintColor = Color.green;
        }

        if (player == 2)
        {
            if (GameManager.p2Color == "red")
                paintColor = Color.red;
            if (GameManager.p2Color == "blue")
                paintColor = Color.blue;
            if (GameManager.p2Color == "yellow")
                paintColor = Color.yellow;
            if (GameManager.p2Color == "green")
                paintColor = Color.green;
        }
    }

    void Respawn()
    {
        if(player == 1)
            transform.position = checkpointPos + new Vector3(0, .25f, 0) + transform.forward * 2;

        if(player == 2)
            transform.position = checkpointPos + new Vector3(0, .25f, 0) + -transform.forward * 2;

        transform.rotation = checkpointRot;
        respawnCount += 1;
        if(respawnCount > 10)
            transform.rotation = Quaternion.Euler(0, lastYRot, 90);
    }

    IEnumerator TimedAirControl()
    {
        //Use a loop to check every second if the player has started touching the ground
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(.25f);
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
        yield return new WaitForSeconds(2);
        if (!canControl)
        {
            Respawn();
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator ActivationTimer()
    {
        canActivateFinish = false;
        yield return new WaitForSeconds(15);
        canActivateFinish = true;
    }

    IEnumerator PaintTrack()
    {
        RaycastHit hit;
        if (Physics.Raycast(wheelRay, out hit, 0.7f))
        {
            if (hit.transform.GetComponent<MeshRenderer>().material.color != paintColor)
            {
                if (hit.transform.gameObject.layer == 8)
                    Destroy(hit.transform.gameObject);
                GameObject newPaintMark = Instantiate(paintMark, hit.point, Quaternion.Euler(0, transform.rotation.y, 0)) as GameObject;
                newPaintMark.GetComponent<MeshRenderer>().material.color = paintColor;
            }
        }
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator Decelarate()
    {
        if(currentSpeed > 0)
            currentSpeed -= 4;

        if (currentSpeed < 0)
            currentSpeed = 0;

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Accelerate()
    {
        if(currentSpeed < topSpeed)
            currentSpeed += acceleration;

        if (currentSpeed > topSpeed)
            currentSpeed = topSpeed;

        yield return new WaitForSeconds(0.5f);
    }

}
