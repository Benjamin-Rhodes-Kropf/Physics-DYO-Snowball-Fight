using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class VelocityCalculator : MonoBehaviour
{
    public GameObject bodySourceView;

    public GameObject snowBall;
    public Transform MidOfBody;
    public Transform myTrans;
    public Transform endMarker;

    public float cooldownTime;
    //lerp to goal
    public Vector3 startingVel;
    public Vector3 endingVel; 
    public Vector3 previous;
    public Vector3 velocity;
    public Vector3 BallVelocity;

    public bool rightHand;
    public bool hasSnowball;


    public bool spaceKeyPressed;
    private bool hasCooledDown;
    // Movement speed in units per second.
    public float speed = 5.0F;

    // Start is called before the first frame update
    void Start()
    {
        hasCooledDown = true;
    }

    // Update is called once per frame
    void Update()
    {
       startingVel = myTrans.position;
       endingVel = myTrans.position;
    // Set our position as a fraction of the distance between the markers.
    transform.position = Vector3.Lerp(myTrans.position, endMarker.position, Time.deltaTime*speed);

        //check if space key pressed
        //if (Input.GetKeyDown("space") && hasCooledDown == true)
        //{
        //   spaceKeyPressed = true;
        // StartCoroutine(SpaceKey());
        //}
        if(transform.position.y < 3.8f)
        {
            hasSnowball = true;
        }
        if (hasSnowball)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        else {
            GetComponent<MeshRenderer>().enabled = false;
        }

        if (bodySourceView.GetComponent<BodySourceView>().leftHandOpen == "Open" && hasSnowball && hasCooledDown == true && velocity.magnitude > 2.5f && rightHand == false) {
            spaceKeyPressed = true;
            StartCoroutine(SpaceKey());
                hasSnowball = false;
        }
        if (bodySourceView.GetComponent<BodySourceView>().rightHandOpen == "Open" && hasSnowball && hasCooledDown == true && velocity.magnitude > 2.5f && rightHand == true)
        {
            spaceKeyPressed = true;
            StartCoroutine(SpaceKey());
            hasSnowball = false;
        }

        //calculate velocity
        Vector3 position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        velocity = (((position-previous)) / Time.deltaTime)*-1;
        previous = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //print(velocity);

    }

    IEnumerator SpaceKey()
    {
        print("space key was pressed");
        spaceKeyPressed = false;
        hasCooledDown = false;
        BallVelocity = velocity;
        createSnowball();
        yield return new WaitForSeconds(cooldownTime);
        hasCooledDown = true;
        print("cooledDown");

    }

    void createSnowball()
    {
        GameObject sB = (GameObject)Instantiate(snowBall, endingVel, Quaternion.identity);//Spawns a copy of ZombiePrefab at SpawnPoint
        sB.GetComponent<SnowBall>().setVelocity(BallVelocity);
    }
}
