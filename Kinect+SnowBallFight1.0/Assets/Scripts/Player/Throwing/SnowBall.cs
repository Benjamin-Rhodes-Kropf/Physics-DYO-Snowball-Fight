using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public Rigidbody myRb;
    public Vector3 velocity;
    public MeshRenderer meshRenderer;
    public float velocityScaler;
    public bool hasBeenSet = false;
    public GameObject particle;

    [SerializeField]
    public float radius;
    public float force;
    public void setVelocity(Vector3 vel)
    {
        velocity = vel;
        myRb.velocity = vel;
        myRb.velocity *= -1;
        myRb.velocity *= velocityScaler;
        //less up
        myRb.velocity = new Vector3(myRb.velocity.x, myRb.velocity.y * 0.4f, myRb.velocity.z);
    }

    private void Awake()
    {
        StartCoroutine(NoCollideDelete());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            
        }
        //explode things near by
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider objectclose in colliders)
        {
            Rigidbody rb = objectclose.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Debug.Log(collision.gameObject.name);
        meshRenderer.enabled = false;
        particle.SetActive(true);
        myRb.constraints = RigidbodyConstraints.FreezeRotation;
        myRb.constraints = RigidbodyConstraints.FreezePosition;
        Debug.Log("collided");
        StartCoroutine(WaitAndRemoveMyself());
    }

    IEnumerator WaitAndRemoveMyself()
    {
        yield return new WaitForSeconds(3f);
        Object.Destroy(this.gameObject);
    }

    IEnumerator NoCollideDelete()
    {
        yield return new WaitForSeconds(10f);
        Object.Destroy(this.gameObject);
    }
}
