using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 the goal for this script is to obtain the nessisary joint rotational values
 from Xbox Kinect so that we may make a physics based character that tries to follow the kinect.
*/

public class FindJointRotation : MonoBehaviour
{
    //set to true
    public bool isGettingData = true;
    public bool gotData;
    public float RotationSpeed;

    public float Debugrot;

    // Left
    private GameObject HandLeft;
    private GameObject ElbowLeft;
    private GameObject ShoulderLeft;
    private GameObject HipLeft;
    private GameObject KneeLeft;
    private GameObject FootLeft;

    // Right
    private GameObject HandRight;
    private GameObject ElbowRight;
    private GameObject ShoulderRight;
    private GameObject HipRight;
    private GameObject KneeRight;
    private GameObject FootRight;

    // mid
    private GameObject SpineBase;
    private GameObject SpineMid;
    private GameObject ShoulderMid;

    //up
    private GameObject Head;


    //  values that will be set in the Inspector

    //  the "target" refers to the position of the kinects players body
    private Transform ElbowLeftTarget;
    private Transform ShoulderLeftTarget;
    private Transform ElbowRightTarget;
    private Transform ShoulderRightTarget;
    

    // our red cubes(or clones of the kinects joints)
    public GameObject ElbowLeftClone;
    public GameObject ShoulderLeftClone;
    public GameObject ElbowRightClone;
    public GameObject ShoulderRightClone;
    public GameObject HipLeftClone;
    public GameObject HipRightClone;
    public GameObject KneeRightClone;
    public GameObject KneeLeftClone;
    public GameObject SpineBaseHipClone;
    public GameObject SpineBaseClone;
    public GameObject SpineMidClone;
    public GameObject SpineShoulderMidClone;


    public GameObject HeadClone;
    public GameObject HandLeftClone;
    public GameObject HandRightClone;

    // values for internal use
    private Quaternion ElbowLeft_LookRotation;
    private Quaternion ShoulderLeft_LookRotation;
    private Quaternion ElbowRight_LookRotation;
    private Quaternion ShoulderRight_LookRotation;


    //  spine
    private Vector3 ElbowLeft_TargetDirection;
    private Vector3 ShoulderLeft_TargetDirection;
    private Vector3 ElbowRight_TargetDirection;
    private Vector3 ShoulderRight_TargetDirection;
   

    // spine
  

    private void Start()
    {
        isGettingData = true;
    }

    private void Update()
    {


        if (isGettingData == true || HandLeft == null)
        {
            isGettingData = false;
            GetData();

        }
        else
        {



            //    sets the clones position to the true kinects joints position
            ElbowLeftClone.transform.position = ElbowLeft.transform.position;
            ShoulderLeftClone.transform.position = ShoulderLeft.transform.position;
            ElbowRightClone.transform.position = ElbowRight.transform.position;
            ShoulderRightClone.transform.position = ShoulderRight.transform.position;
            HipLeftClone.transform.position = HipLeft.transform.position;
            HipRightClone.transform.position = HipRight.transform.position;
            KneeRightClone.transform.position = KneeRight.transform.position;
            KneeLeftClone.transform.position = KneeLeft.transform.position;
            SpineShoulderMidClone.transform.position = ShoulderMid.transform.position;

            //    spine
            SpineMidClone.transform.position = SpineMid.transform.position;
            SpineBaseClone.transform.position = SpineBase.transform.position;

            //head
            HeadClone.transform.position = Vector3.Lerp(HeadClone.transform.position, Head.transform.position, Time.deltaTime*10);
            HandLeftClone.transform.position = HandLeft.transform.position;
            HandRightClone.transform.position = HandRight.transform.position;


            //    sets the spine base to inbetween the hips


            // find the vector pointing from our position (the xbox kinects joints) to the target (for example the elbows target is the hand)
            ElbowLeft_TargetDirection = (ElbowLeftTarget.position - ElbowLeftClone.transform.position).normalized;
            ShoulderLeft_TargetDirection = (ShoulderLeftTarget.position - ShoulderLeftClone.transform.position).normalized;
            ElbowRight_TargetDirection = (ElbowRightTarget.position - ElbowRightClone.transform.position).normalized;
            ShoulderRight_TargetDirection = (ShoulderRightTarget.position - ShoulderRightClone.transform.position).normalized;





            // create the rotation we need to be in to look at the target
            ElbowLeft_LookRotation = Quaternion.LookRotation(ElbowLeft_TargetDirection);
            ShoulderLeft_LookRotation = Quaternion.LookRotation(ShoulderLeft_TargetDirection, new Vector3(0, 1, 0));
            ElbowRight_LookRotation = Quaternion.LookRotation(ElbowRight_TargetDirection);


            // spine
         

            // rotate us over time according to speed until we are in the required rotation
            ElbowLeftClone.transform.rotation = Quaternion.Slerp(ElbowLeftClone.transform.rotation, ElbowLeft_LookRotation, Time.deltaTime * 10f);
            ShoulderLeftClone.transform.rotation = Quaternion.Slerp(ShoulderLeftClone.transform.rotation, ShoulderLeft_LookRotation, Time.deltaTime * RotationSpeed);
            ElbowRightClone.transform.rotation = Quaternion.Slerp(ElbowRightClone.transform.rotation, ElbowRight_LookRotation, Time.deltaTime * RotationSpeed);
            ShoulderRightClone.transform.rotation = Quaternion.Slerp(ShoulderRightClone.transform.rotation, ShoulderRight_LookRotation, Time.deltaTime * RotationSpeed);
           
            // spine
            //SpineBaseHipClone.transform.rotation = Quaternion.Slerp(SpineBaseHipClone.transform.rotation, SpineBaseHip_LookRotation, Time.deltaTime * RotationSpeed);
         
         

        }

    }

    void GetData()
    {
        
        //Debug.Log("searching....");
        //  finds game objects in higherarchy and sets then equal to our game objects
        HandLeft = GameObject.Find("HandLeft");
        ElbowLeft = GameObject.Find("ElbowLeft");
        ShoulderLeft = GameObject.Find("ShoulderLeft");
        HandRight = GameObject.Find("HandRight");
        ElbowRight = GameObject.Find("ElbowRight");
        ShoulderRight = GameObject.Find("ShoulderRight");
        HipLeft = GameObject.Find("HipLeft");
        HipRight = GameObject.Find("HipRight");
        KneeRight = GameObject.Find("KneeRight");
        KneeLeft = GameObject.Find("KneeLeft");
        FootRight = GameObject.Find("FootRight");
        FootLeft = GameObject.Find("FootLeft");
        ShoulderMid = GameObject.Find("SpineShoulder");
        SpineMid = GameObject.Find("SpineMid");
        SpineBase = GameObject.Find("SpineBase");
        Head = GameObject.Find("Head");



        if (HandLeft != null)
        {
            gotData = true;
            //sets the targets (end joints for example hands dont get targets or clones)
            ElbowLeftTarget = HandLeft.transform;
            ShoulderLeftTarget = ElbowLeft.transform;
            ElbowRightTarget = HandRight.transform;
            ShoulderRightTarget = ElbowRight.transform;


            //   figures out if we are still serching for a player
            if (HandLeft.gameObject.transform.position != new Vector3(0, 0, 0))
            {
                isGettingData = false;
                Debug.Log("DoneSerching!");
            }
        }
    }
}