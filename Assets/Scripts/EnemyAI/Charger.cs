using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : Enemy
{
    private bool charge = false;
    private bool recharging = false;
    private float chargeSpeed = 50f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (recharging)
        {
            transform.rotation = GetEnemyDir();
        }
        if (!charge)
        {
            charge = true;
            bloxitRigibody.velocity = Vector3.zero;
            bloxitRigibody.AddForce(transform.forward * chargeSpeed, ForceMode.VelocityChange);
            StartCoroutine(ChargeReset());
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
                recharging = true;
                bloxitRigibody.velocity = Vector3.zero;
                yield return StartCoroutine(ChargeCooldown());
            }
        }
    }
    IEnumerator ChargeCooldown()
    {
        while (charge)
        {
            yield return new WaitForSeconds(2f);
            recharging = false;
            charge = false;
        }
    }
}
