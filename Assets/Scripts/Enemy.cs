using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Bloxit
{
    public GameObject exhaustBar;

    public float maxExhaust = 100f;
    public float exhaust = 0f;

    public float worth = 20f;

    protected GameObject player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        BloxitInit();
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    public virtual void Update()
    {
        bloxitRigibody.velocity *= 0.99f;        
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        UpdateExhaust();
        if (!onGround)
        {
            BloxitUpdate();
        }
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            float colliderExhast = gameObject.GetComponent<Enemy>().exhaust;
            gameObject.GetComponent<Enemy>().exhaust += collision.relativeVelocity.magnitude / 10f;
            Vector3 lookDirection = (transform.position - collision.transform.position).normalized;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(collision.gameObject.GetComponent<Rigidbody>().velocity.x, 0, collision.gameObject.GetComponent<Rigidbody>().velocity.z) * (2 + colliderExhast));
        }
    }
    public virtual void OnDestroy()
    {
        gameManager.money += Mathf.RoundToInt(worth * Random.Range(0.8f, 1.2f));
        gameManager.kills++;
        gameManager.UpdateEnemies();
    }
    protected void UpdateExhaust()
    {
        exhaust = Mathf.Clamp(exhaust, 0, maxExhaust);
        exhaustBar.GetComponent<Slider>().value = exhaust / maxExhaust;
        exhaust -= Mathf.Sqrt(exhaust) * Time.deltaTime;
    }
    protected Quaternion GetEnemyDir()
    {
        if (player.GetComponent<Player>().onGround)
        {
            return Quaternion.LookRotation(player.transform.position - transform.position);
        }
        return transform.rotation;
    }
}
