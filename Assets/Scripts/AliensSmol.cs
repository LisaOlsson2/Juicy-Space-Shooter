using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliensSmol : AliensBase
{

    Vector3 direction;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        speed = 60;
        direction = (keeper.ship.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    public override void Update()
    {
        // changes it's direction to aim for the player every time it reaches a border
        if (Mathf.Abs(transform.position.x) > keeper.borderH || Mathf.Abs(transform.position.y) > keeper.borderV)
        {
            direction = (keeper.ship.transform.position - transform.position).normalized;
            // i'll make the rotation change later
        }
        transform.position += direction * Time.deltaTime * speed;
    }
}
