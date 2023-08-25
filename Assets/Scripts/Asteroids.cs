using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroids : AliensBase
{
    Vector3 astroidDirection; // the direction in which the asteroid moves.
    float timer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        speed = 10;
        astroidDirection = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        transform.position += astroidDirection.normalized * Time.deltaTime * speed;
        timer += Time.deltaTime;
        if (timer > 0.5) // it switches direction every half second and it's always random, unlike the aliens who all move towards the player
        {
            astroidDirection.x = Random.Range(-10f, 10f);
            astroidDirection.y = Random.Range(-10f, 10f);
            timer = 0;
        }
    }
}