using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliensFisk : AliensBase
{
    float timer;
    Vector3 direction;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        speed = 10;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        transform.position += direction * Time.deltaTime * speed;

        if (timer > 1) // the direction updates every second to face the player
        {
            direction = (keeper.ship.transform.position - transform.position).normalized;
            timer = 0;
        }

    }
}
