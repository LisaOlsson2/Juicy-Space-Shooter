using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    // THIS ISN'T DONE YET

    float timer;
    SpawnerAndValueKeeperSmol keeper;
    readonly float lifeTime = 4;
    readonly float speed = 10;
    float camTimer;

    // Start is called before the first frame update
    void Start()
    {
        keeper = FindObjectOfType<SpawnerAndValueKeeperSmol>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (camTimer > 0.2)
        {

        }

        if (timer > lifeTime)
        {
            keeper.beaming = false;
            Destroy(gameObject);
        }


        transform.localScale += Vector3.right * Time.deltaTime * speed;
        transform.position = keeper.ship.transform.position + keeper.skottDirection.normalized * 3 + keeper.skottDirection.normalized * transform.localScale.x / 2;
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan(keeper.skottDirection.y/keeper.skottDirection.x) * Mathf.Rad2Deg);

    }
}
