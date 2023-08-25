using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MovingObjectsBase
{
    [SerializeField]
    GameObject square; // the only purpose of this thing is to look cool

    float dashTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartAllObjects();
        speed = 20;
        square.GetComponent<ColorChange>().colorSpeed = 0.1f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        square.transform.position -= (square.transform.position - transform.position) * Time.deltaTime * speed; // it becomes faster the further away it is from the player and they have the same speed when the distance between them is 1 causing it to never go too far behind the player, except for when it dashes or teleports at the borders, but since the distance then becomes greater it'll return rather quickly, which i think looks pretty cool

        if (!keeper.beaming) // can't move if the beam is active
        {
            dashTimer += Time.deltaTime;

            if (dashTimer > 0.7 && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Dash(3000, 0.15f));
                dashTimer = 0;
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) && (!move || (move && transform.position.y > -keeper.borderV))) // can't move below the negative vertical border while move is true
            {
                transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            }
        }

        if (transform.position.y > 75)
        {
            keeper.LevelUp();
        }
    }

    public override IEnumerator Damage(GameObject collidingObject, int changes)
    {
        StartCoroutine(Knockback(transform.position - collidingObject.transform.position, 900, 0.2f));                                
        if (collidingObject.layer == 6)
        {
            keeper.hP--;
            for (int i = 0; i < changes;) // shakes the cam changes times
            {
                yield return new WaitForSeconds(0.04f);
                keeper.cam.transform.position += new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
                spriteRenderer.color = new Vector4(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
                yield return new WaitForSeconds(0.04f);
                keeper.cam.transform.position = new Vector3(transform.position.x, transform.position.y, -25);
                i++;
            }
            spriteRenderer.color = Color.white;
        }
    }

    private IEnumerator Dash(float speed, float time)
    {
        rb.AddForce(keeper.skottDirection.normalized * speed);
        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
    }
}