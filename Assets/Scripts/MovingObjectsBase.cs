using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectsBase : MonoBehaviour
{
    // i think everything that happens in this script is pretty straight forward


    public SpawnerAndValueKeeperSmol keeper;
    public float speed;
    public bool move; // true if the player can go on to the next level
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    public void StartAllObjects()
    {
        keeper = FindObjectOfType<SpawnerAndValueKeeperSmol>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (transform.position.x > keeper.borderH)
        {
            transform.position = new Vector3(-keeper.borderH, transform.position.y, 0);
        }
        if (transform.position.x < -keeper.borderH)
        {
            transform.position = new Vector3(keeper.borderH, transform.position.y, 0);
        }

        // these only teleport the player if move is false so that it can move up when it's true
        if (transform.position.y < -keeper.borderV && !move)
        {
            transform.position = new Vector3(transform.position.x, keeper.borderV, 0);
        }
        if (transform.position.y > keeper.borderV && !move) 
        {
            transform.position = new Vector3(transform.position.x, -keeper.borderV, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision(collision.gameObject); // temp
        StartCoroutine(Damage(collision.gameObject, 7));
    }

    public virtual IEnumerator Damage(GameObject collidingObject, int changes)
    {
        yield return new WaitForSeconds(1); // this only exists because it has to return something
    }

    public virtual void Collision(GameObject collidingObject)
    {

    }
    public IEnumerator Knockback(Vector3 direction, float intensity, float duration)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * intensity);
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector3.zero;
    }
}