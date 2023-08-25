using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skott : MovingObjectsBase
{
    public Vector3 skottDirection;

    float deathTimer;
    readonly float life = 7;

    // Start is called before the first frame update
    void Start()
    {
        StartAllObjects();
        skottDirection = keeper.skottDirection.normalized;
        speed = 37;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        transform.position += skottDirection * Time.deltaTime * speed;
        deathTimer += Time.deltaTime;

        if (deathTimer > life) // kills the bullet when it's been alive for however long life is
        {
            Destroy(gameObject);
        }
    }
    public override void Collision(GameObject collidingObject)
    {
        if (collidingObject.layer == 6)
        {
            collidingObject.GetComponent<Rigidbody2D>().AddForce(skottDirection * 400);
        }
        Destroy(gameObject);
    }
}