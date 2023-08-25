using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aliens : AliensBase
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        speed = 10;
        GetComponent<Rigidbody2D>().velocity = Vector3.down * speed;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();


        // makes it approach the player
        if (transform.position.x > keeper.ship.transform.position.x)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }

}