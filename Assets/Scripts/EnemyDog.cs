using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// INHERITANCE
public class EnemyDog : Enemy
{
    private float moveSpeed = 9.0f;

    // POLYMORPHISM
    public override void ChasePlayer()
    {
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }


}
