using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject menuCam;
    public GameObject playerCam;

    [Header ("KINECT")]
    public GameObject KinectBody;

    [Header("UIManager")]
    public GameObject uiManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaitForPlayer() {
        //WAIT UNTIL PLAYER FOUND
        StartCoroutine(WaitforPlayer());
    }
    IEnumerator WaitforPlayer()
    {
        Debug.Log("wait" + KinectBody.GetComponent<FindJointRotation>().gotData);
        while (!KinectBody.GetComponent<FindJointRotation>().gotData)
        {
            Debug.Log("waiting!" + KinectBody.GetComponent<FindJointRotation>().gotData);
            yield return new WaitForSeconds(0.3f);
        }
        Debug.Log("Play");
        Play();
    }

    public void Play()
    {
        StartCoroutine(StartGame());
        uiManager.GetComponent<UIManager>().Play();
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        menuCam.active = false;
        playerCam.active = true;
    }
}
