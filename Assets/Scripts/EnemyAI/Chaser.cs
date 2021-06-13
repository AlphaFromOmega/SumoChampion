using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (onGround)
        {
            if (player.GetComponent<Player>().onGround)
            {
                Quaternion lookRot = Quaternion.LookRotation(player.transform.position - transform.position);
                lookRot.z = 0;
                transform.rotation = lookRot;
                bloxitRigibody.AddForce(transform.forward * speed / Mathf.Max(1, exhaust / 8) * Time.deltaTime, ForceMode.Impulse);
            }
        }
        else
        {
            BloxitUpdate();
        }
    }
}
