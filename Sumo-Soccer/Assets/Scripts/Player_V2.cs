/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_V2 : MonoBehaviour
{
    public bool BOT;

    [Header("Base attributes")]
    //[Tooltip("Test thsfsdfgfsd")]
    public int pNum;
    public GameObject myMesh;
    private Rigidbody rb;
    public GameObject Pointer;
    private GameObject pointy;
    public GameObject Hand;
    public GameObject Ball;
    private BallControl bc;
    public List<GameObject> Enemies;
    public bool holding;
    public bool throwing = false;
    public float runSpeed;
    public float sprintSpeed;
    private float SPRINT;
    public float turnSpeed;

    [Header("Current stats")]
    public Vector3 Heading;
    public float joyX;
    public float joyY;
    public float aimX;
    public float aimY;
    public float _RT;
    public float _LT;
    public bool blinkCD = false;
    public float blinkDist;

    void Awake()
    {
        
    }
    void Start()
    {
        GameObject[] allPlayers;
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        Enemies.AddRange(allPlayers);
        Ball = GameObject.FindGameObjectWithTag("Ball");
        Enemies.Remove(gameObject);
        bc = Ball.GetComponent<BallControl>();
        if (!BOT)
        {
            rb = GetComponent<Rigidbody>();
            LoadPlayer();
        }

    }

    void LoadPlayer()
    {
        pointy = Instantiate(Pointer, transform.position, Quaternion.identity);
        pointy.GetComponent<TargetAimer>().myPlayer = gameObject;
        pointy.GetComponent<TargetAimer>().Enemies = Enemies;
    }

    void Update()
    {
        if(!BOT)
        {
            GetInput();
            Rotate();
            Run();
        }
        else
        {
            BOTCODE();
        }

    }

    void GetInput()
    {
        joyX = Input.GetAxis("L_XAxis_" + pNum);
        joyY = Input.GetAxis("L_YAxis_" + pNum);
        _RT = Input.GetAxisRaw("TriggersR_" + pNum);
        _LT = Input.GetAxisRaw("TriggersL_" + pNum);
        SPRINT = sprintSpeed * _RT;

        //BLINK
        if (_LT == 1 && !blinkCD && !holding)
        {
            blinkCD = true;
            StartCoroutine(blink());
        }
        //THROW BALL
        if(_LT == 1 && holding)
        {
            holding = false;
            GameObject target = pointy.GetComponent<TargetAimer>().TARGET;
            bc.ThrowTo(target);
            target.GetComponent<Player_V2>().holding = true;
        }
    }
    IEnumerator blink()
    {
        yield return new WaitForSeconds(.1125f);
        transform.position += transform.forward * blinkDist;
        yield return new WaitForSeconds(2f);
        blinkCD = false;
    }

    void Rotate()
    {
        Heading = new Vector3(0, Mathf.Atan2(joyX, joyY) * 180 / Mathf.PI, 0);
        if (Mathf.Abs(joyX) + Mathf.Abs(joyY) >= 0.1f)
        {
            transform.eulerAngles = Heading;
        }
        //if (Mathf.Abs(joyX + joyY) >= 0.1f)
        //{
        //    float step = turnSpeed * Time.deltaTime;
        //    Vector3 newdir = Vector3.RotateTowards(myMesh.transform.forward, Heading, step / 13, 2f);
        //    myMesh.transform.rotation = Quaternion.LookRotation(newdir, Vector3.up);
        //}

    }

    void Run()
    {
        Vector3 targetVel = transform.forward * (runSpeed + SPRINT);
        if (Mathf.Abs(joyX) + Mathf.Abs(joyY) >= 0.1f)
        {
            //rb.velocity = transform.forward * runSpeed;
            rb.velocity = Vector3.Lerp(rb.velocity, targetVel, Time.deltaTime * turnSpeed);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 1.5f);
        }
        //if (Input.getm)
    }

    void BOTCODE()
    {

        if(holding && !throwing)
        {
            throwing = true;
            holding = false;
            StartCoroutine(BOTthrow(Random.Range(0.5f, 3)));
            print("Starting throw");
        }
    }
    IEnumerator BOTthrow(float timer)
    {
        print("In throw");
        yield return new WaitForSeconds(timer);
        int randomTarget = Random.Range(0, Enemies.Count - 1);

        print("I, "+gameObject.name+" am targeting player index: " + randomTarget);
        GameObject target = Enemies[randomTarget];
        print("Target name is: " + target.name);

        bc.ThrowTo(target);
        print("Throw done");
        print("I, " + gameObject.name + " am thowing the ball to " +target.name);
        throwing = false;
    }



}
*/