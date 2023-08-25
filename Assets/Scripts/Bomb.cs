using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MovingObjectsBase
{
    float timer;

    GameObject child;
    CircleCollider2D childCollider;
    SpriteRenderer childRenderer;
    Color childColor;

    // Start is called before the first frame update
    void Start()
    {
        StartAllObjects();
        speed = 4;

        child = transform.GetChild(0).gameObject; // the round thingy that grows to how far the exposion reaches
        childCollider = child.GetComponent<CircleCollider2D>();
        childRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        childColor = childRenderer.color;
        child.SetActive(false);
    }
    // Update is called once per frame
    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer < 0.7)
        {
            transform.position += keeper.skottDirection.normalized * Time.deltaTime * 4;
        }

        if (timer > 1.5) // when the bomb goes off
        {
            child.SetActive(true);
            if (child.transform.localScale.y < 7)
            {
                child.transform.localScale += new Vector3(3, 3, 0) * Time.deltaTime; // grows pretty slowly at first
            }
            else if (child.transform.localScale.y < 20)
            {
                child.transform.localScale += new Vector3(40, 40, 0) * Time.deltaTime; // and then really fast
            }
            else
            {
                childCollider.enabled = true; // enables the collider
                childRenderer.color = childColor;
                childColor.a -= Time.deltaTime * 0.1f; // makes the color fade (just makes it more transparent)
                if (childColor.a <= 0) 
                {
                    Destroy(gameObject); // destroys it when it has faded completely
                }
            }
        }
    }
}