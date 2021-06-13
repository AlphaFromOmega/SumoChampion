using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Bloxit
{
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float chargeSpeed = 1;

    private Vector3 controllerDirection = Vector3.zero;
    private bool charge = false;

    // Start is called before the first frame update
    void Start()
    {
        BloxitInit();
    }

    // Update is called once per frame
    void Update()
    {
        bloxitRigibody.velocity *= 0.99f;
        ControllerMovement();
        if (!onGround)
        {
            BloxitUpdate();
        }
    }

    private void ControllerMovement()
    {
        if (!charge)
        {
            controllerDirection = Vector3.zero;
            controllerDirection.x = Input.GetAxis("Horizontal");
            controllerDirection.z = Input.GetAxis("Vertical");

            Vector3 movementDirection = (cameraObject.transform.forward * controllerDirection.z + cameraObject.transform.right * controllerDirection.x).normalized;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.rotation = Quaternion.LookRotation(movementDirection);
                charge = true;
                bloxitRigibody.velocity = Vector3.zero;
                bloxitRigibody.AddForce(transform.forward * chargeSpeed, ForceMode.VelocityChange);
                StartCoroutine(ChargeReset());
            }
            else
            {
                if (Math.Abs(controllerDirection.x) > 0.1 || Math.Abs(controllerDirection.z) > 0.1)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movementDirection), speed * 50f * Time.deltaTime);
                    bloxitRigibody.AddForce(movementDirection * speed * Time.deltaTime, ForceMode.Impulse);
                }
            }        
        }
    }
    IEnumerator ChargeReset()
    {
        while (charge)
        {
            yield return new WaitForSeconds(0.5f);
            bloxitRigibody.velocity *= 0.5f;
            bool velCheck = (Math.Abs(bloxitRigibody.velocity.x) < 10 && Math.Abs(bloxitRigibody.velocity.z) < 10);
            yield return velCheck;
            if (velCheck)
            {
                bloxitRigibody.velocity = Vector3.zero;
                charge = false;
            }
        }
    }
}
