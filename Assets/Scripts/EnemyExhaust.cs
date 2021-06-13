using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExhaust : MonoBehaviour
{
    public Transform target;
 
    void Update()
    {
        var wantedPos = Camera.main.WorldToScreenPoint(target.position) + new Vector3(0, 50);
        transform.position = wantedPos;
    }
}
