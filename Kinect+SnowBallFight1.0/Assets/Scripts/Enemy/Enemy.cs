using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isDead;
    public float reloadTime;
    public float varitionOnReloadTime;
    public float shootingTime;
    public float varitionOnShootingTime;
    public Vector3 hidingLocation;
    public Vector3 shootingLocation;
    public float speed = 1.0F;


    private Vector3 endMarker;



    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        StartCoroutine(ShootAndReload());
    }

    // Move to the target end position.
    void Update()
    {
        if (isDead)
        {
            StopCoroutine(ShootAndReload());
        }

        // Set our position as a fraction of the distance between the markers.
        if (!isDead)
        {
            transform.position = Vector3.Lerp(transform.position, endMarker, Time.deltaTime*speed);
        }
    }

    IEnumerator ShootAndReload()
    {
        endMarker = shootingLocation;
        StartCoroutine(WaitRandomAndFire());
        yield return new WaitForSeconds(shootingTime + Random.Range(0, varitionOnReloadTime));
        endMarker = hidingLocation;
        yield return new WaitForSeconds(reloadTime + Random.Range(0, varitionOnShootingTime));
        StartCoroutine(ShootAndReload());
    }
    IEnumerator WaitRandomAndFire()
    {
        yield return new WaitForSeconds(Random.RandomRange(shootingTime/4,shootingTime));
        print("fire!");
    }
}
