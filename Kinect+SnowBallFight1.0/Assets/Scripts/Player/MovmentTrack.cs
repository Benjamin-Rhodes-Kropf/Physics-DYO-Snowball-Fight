using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentTrack : MonoBehaviour
{
    public GameObject Base;
    public GameObject Mid;
    public GameObject Head;

    //left
    public GameObject LeftElbow;
    public GameObject LeftShoulder;
    public GameObject LeftHand;

    //right
    public GameObject RightElbow;
    public GameObject RightShoulder;
    public GameObject RightHand;

    public Transform BaseClone;
    public Transform MidClone;
    public Transform HeadClone;

    public Transform LeftElbowClone;
    public Transform LeftShoulderClone;
    public Transform LeftHandClone;

    public Transform RightElbowClone;
    public Transform RightShoulderClone;
    public Transform RightHandClone;

    [SerializeField] private FindJointRotation _movmentTrack;
    [SerializeField] private bool hasRecognizedPlayer = false;
    [SerializeField] private bool moveWithPlayer = false;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private  float playerMovmentSpeed = 10;
    [SerializeField] private float playerTurnSpeed = 10;

    private void Start()
    {
        
    }

    void Update()
    {
        CheckIfRecognizedPlayer();
        UpdatePlayerPosition();
        CheckForPlayerTurning();
        UpdatePlayerMoving();
        StayGrounded();
    }

    void CheckForPlayerTurning()
    {
        //if left hand x is close to zero and right hand x is greater than zero then turn left
        Debug.Log("Left hand chest distance: " + LeftHand.GetComponent<Transform>().localPosition.x);
        Debug.Log("Right hand chest distance: " + RightHand.GetComponent<Transform>().localPosition.x);
        if ( LeftHand.GetComponent<Transform>().localPosition.x > 2 && Math.Abs(RightHand.GetComponent<Transform>().localPosition.x) < 1f)
        {
            // Trigger camera turn
            Debug.Log("Camera turn triggered left!");
            
            //set player turn speed based on right hand z position offset by intial z postion
            float playerTurnForce = Math.Abs(LeftHand.GetComponent<Transform>().localPosition.z - initialPosition.z);
            
            //turn the player left slowly
            transform.Rotate(Vector3.up, -Time.deltaTime * playerTurnSpeed * (playerTurnForce+1));
            
        }
        else if (RightHand.GetComponent<Transform>().localPosition.x < -2 && Math.Abs(LeftHand.GetComponent<Transform>().localPosition.x) < 1f)
        {
            // Trigger camera turn
            Debug.Log("Camera turn triggered right!");
            
            //set player turn speed based on right hand z position offset by intial z postion
            float playerTurnForce = Math.Abs(RightHand.GetComponent<Transform>().localPosition.z - initialPosition.z);
            
            //turn the player right slowly
            transform.Rotate(Vector3.up, Time.deltaTime * playerTurnSpeed * (playerTurnForce + 1));
        }
    }

    void UpdatePlayerPosition()
    {
        //set snowman to kinect position
        Base.GetComponent<Transform>().localPosition = Vector3.Lerp(Base.GetComponent<Transform>().localPosition, BaseClone.position, Time.deltaTime*5);
        Mid.GetComponent<Transform>().localPosition = Vector3.Lerp(Mid.GetComponent<Transform>().localPosition, MidClone.position + new Vector3(0, 0.5f, 0), Time.deltaTime * 5);
        Head.GetComponent<Transform>().localPosition = HeadClone.position + new Vector3(0,0,0);

        //arms
        LeftElbow.GetComponent<Transform>().localPosition = Vector3.Lerp(LeftElbow.GetComponent<Transform>().localPosition,LeftElbowClone.transform.position, Time.deltaTime*10);
        LeftShoulder.GetComponent<Transform>().localPosition = Vector3.Lerp(LeftShoulder.GetComponent<Transform>().localPosition, LeftShoulderClone.transform.position, Time.deltaTime * 10);
        LeftHand.GetComponent<Transform>().localPosition = Vector3.Lerp(LeftHand.GetComponent<Transform>().localPosition, LeftHandClone.transform.position, Time.deltaTime * 10);

        RightElbow.GetComponent<Transform>().localPosition = Vector3.Lerp(RightElbow.GetComponent<Transform>().localPosition, RightElbowClone.transform.position, Time.deltaTime * 10);
        RightShoulder.GetComponent<Transform>().localPosition = Vector3.Lerp(RightShoulder.GetComponent<Transform>().localPosition, RightShoulderClone.transform.position, Time.deltaTime * 10);
        RightHand.GetComponent<Transform>().localPosition = Vector3.Lerp(RightHand.GetComponent<Transform>().localPosition, RightHandClone.transform.position, Time.deltaTime * 10);
    }
    
    void CheckIfRecognizedPlayer()
    {
        if(_movmentTrack.gotData && !hasRecognizedPlayer)
        {
            //set inial position and onnly do this once
            hasRecognizedPlayer = true; 
            StartCoroutine(SetInitialPosition());
        }
    }
    
    //corutine to set intial position
    IEnumerator SetInitialPosition()
    {
        yield return new WaitForSeconds(1);
        initialPosition = Base.GetComponent<Transform>().localPosition;
        moveWithPlayer = true;
    }

    void UpdatePlayerMoving()
    {
        if (moveWithPlayer)
        {
            //if the player moves outside of a RADIUS around the initial position then we move the player in that direction
            float radius = 0.75f;
            float distance = Vector3.Distance(Base.GetComponent<Transform>().localPosition, initialPosition);

            if (distance > radius)
            {
                Vector3 direction = (Base.GetComponent<Transform>().localPosition - initialPosition);
                direction = transform.TransformDirection(direction); // Transform direction to world space
                transform.localPosition += direction * Time.deltaTime * playerMovmentSpeed;
            }
        }
    }

    void StayGrounded()
    {
        RaycastHit hit;
        Debug.DrawRay(Base.GetComponent<Transform>().position + new Vector3(0, -1, 0), -Vector3.up*5, new Color(100,100,100));
        if (Physics.Raycast(Base.GetComponent<Transform>().position + new Vector3(0, -0.2f,0), -Vector3.up, out hit))
            //Debug.Log("Found an object - distance: " + hit.distance);
            if(hit.collider.tag == "Water")
            {
                transform.position += new Vector3(0,10,0);
            }
        transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0,hit.distance-0.25f,0), Time.deltaTime*10);
    }
}
