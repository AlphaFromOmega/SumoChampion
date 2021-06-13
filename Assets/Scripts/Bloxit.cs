using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloxit : MonoBehaviour
{
    protected Vector3 bloxitVelocity;
    protected Rigidbody bloxitRigibody;
    [SerializeField] protected float speed = 1;

    protected GameManager gameManager;

    public bool onGround = true;


    // Start is called before the first frame update
    protected void BloxitInit()
    {
        bloxitRigibody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void Start()
    {
        BloxitInit();
    }
    // Update is called once per frame
    public virtual void Update()
    {
        if (!onGround)
        {
            BloxitUpdate();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Floor")
        {
            Debug.Log(gameObject.name + " collided with floor");
            onGround = true;
        }
    }

    protected void BloxitUpdate()
    {
        bloxitRigibody.AddForce(Vector3.down * 10f);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            onGround = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            onGround = true;
        }
    }
    public static float KineticEnergy(Rigidbody rb)
    {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }
}
