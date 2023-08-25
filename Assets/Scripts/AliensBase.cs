using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliensBase : MovingObjectsBase
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        StartAllObjects();
    }
    public override IEnumerator Damage(GameObject collidingObject, int changes)
    {
        if (collidingObject.layer == 8) // if it's getting shot or bombed or whatever, those are the only weapons i have this far
        {
            keeper.score++;
            speed = 0;
            StartCoroutine(Knockback(transform.position - collidingObject.transform.position, 800, 0.15f));
            for (changes--; changes > 0;) // makes it switch colors changes times
            {
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                changes--;
            }
            if (keeper.score >= keeper.alienMax) // if the last alien has been killed
            {
                keeper.ship.move = true; // the player can move up
                keeper.oldBackground = keeper.currentBackground; // makes the current background the old background so that it can get deleted when the player has moved up
                keeper.currentBackground = Instantiate(keeper.background, new Vector3(0, 104, 6), Quaternion.identity); // instantiaites a new background and makes it the current background
                keeper.currentBackground.GetComponent<SpriteRenderer>().color = new Vector4(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1); // changes the color of the current background
            }
            Destroy(gameObject);
        }
    }
}
