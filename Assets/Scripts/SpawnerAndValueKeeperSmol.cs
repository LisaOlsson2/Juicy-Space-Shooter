using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerAndValueKeeperSmol : MonoBehaviour // had to make everything a bit smaller
{

    public MovingObjectsBase ship; // a script in the ship that contains a function that will be called through this script, its transform will be used in other scripts
    public GameObject cam; // this used to be private and on the ship script, but i needed it for something here, but then i removed it, but i think i'll make it again so ima keep it like this


    // leveling up

    public GameObject background;
    public GameObject currentBackground;
    public GameObject oldBackground;


    // UI
    
    [SerializeField]
    Canvas canvas;
    
    Text text;

    RectTransform beamThingy; // the thingy indicating how charged up the beam is

    public int hP = 30; // at the moment, nothing happens when this depletes
    public int score;


    // aliens

    [SerializeField]
    GameObject[] aliens;
    // [0] aliens, [1] asteroids, [2] smolAliens, [3], fishAliens

    public readonly float borderH = 63; // teleports the player to the negative at the positive and vice versa
    public readonly float borderV = 35.5f; // same as borderH, but stops the player at the negative and lets them through at the positive while leveling up

    public int alienMax = 30; // it's a bit annoying that i have to change this and the HP in the editor
    readonly float alienFrequency = 0.7f; // gonna make it a bit more random later
    int aliensSpawned;

    Vector3 alienSpawn; // where the alien spawns
    

    // skott

    [SerializeField]
    GameObject beampart; // currently the whole beam
    // DON'T CHARGE IT UP IT'S NOT FINISHED

    [SerializeField]
    GameObject[] weapons;
    // [0] gun, [1] bomb

    int vapen; // represents the current weapon's place in the array
    int flip; // the rotation didn't really work and the easiest way to fix it was to just add 180 degrees in those cases
    public bool beaming;

    Vector3 resMiddle; // i would've made the camera a bit slower than the player if i wasn't using this to shoot, but i don't have very much time left so i'm just gonna leave it like this
    public Vector3 skottDirection;

    readonly float beamTime = 1; // the time it takes to charge up the beam
    readonly float reloadTime = 0.1f;


    //Timers
    float alienSpawnTimer;
    float beamTimer;
    float shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        text = canvas.transform.GetChild(0).GetComponent<Text>();
        beamThingy = canvas.transform.GetChild(2).GetComponent<RectTransform>();
        resMiddle = new Vector3(Screen.width, Screen.height, 0) / 2;
        cam = ship.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        text.text = "Score: " + score + "\nHP: " + hP;
        beamThingy.localScale = new Vector3(315 / beamTime * beamTimer, 10, 1);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // pause
        }

        // weaponry


        // switches weapon
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            vapen++;
            if (vapen >= weapons.Length)
            {
                vapen = 0;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && !beaming) // can't if it's active already
        {
            beamTimer += Time.deltaTime; // charges up the beam
        }
        else if (beamTimer > 0.02)
        {
            beamTimer -= Time.deltaTime; // drains the beam
        }

        if (beamTimer > beamTime) // the beam activates as soon as it's charged up
        {
            Instantiate(beampart, ship.transform.position + skottDirection.normalized * 3, Quaternion.Euler(0, 0, Mathf.Atan(skottDirection.y / skottDirection.x) * Mathf.Rad2Deg));
            StartCoroutine(ship.Knockback(-skottDirection.normalized, 1000, 0.2f));
            beaming = true;
            beamTimer = 0;
        }

        skottDirection = Input.mousePosition - resMiddle;
        shootingTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && shootingTimer > reloadTime)
        {
            if (skottDirection.x < 0)
            {
                flip = 180;
            }
            else
            {
                flip = 0;
            }
            Instantiate(weapons[vapen], ship.transform.position + skottDirection.normalized * 3, Quaternion.Euler(0, 0, flip + Mathf.Rad2Deg * Mathf.Atan(skottDirection.y / skottDirection.x)));
            StartCoroutine(ship.Knockback(-skottDirection.normalized, 700, 0.2f));
            shootingTimer = 0;
        }


        // aliens
        if (aliensSpawned < alienMax)
        {
            alienSpawnTimer += Time.deltaTime;
            if (alienSpawnTimer > alienFrequency)
            {
                int r = Random.Range(0, aliens.Length);

                if (r > 0) // aliens[0] always start from the top
                {
                    int r2 = Random.Range(1, 5);
                    if (r2 == 1)
                    {
                        alienSpawn = new Vector3(-borderH, Random.Range(-borderV, borderV), 0);
                    }
                    else if (r2 == 2)
                    {
                        alienSpawn = new Vector3(borderH, Random.Range(-borderV, borderV), 0);
                    }
                    else if (r2 == 3)
                    {
                        alienSpawn = new Vector3(borderV, Random.Range(-borderH, borderH), 0);
                    }
                    else if (r2 == 4)
                    {
                        alienSpawn = new Vector3(-borderV, Random.Range(-borderH, borderH), 0);
                    }
                }
                else
                {
                    alienSpawn = new Vector3(Random.Range(-borderH, borderH), borderV, 0);
                }
                Instantiate(aliens[r], alienSpawn, Quaternion.identity);
                aliensSpawned++;
                alienSpawnTimer = 0;
            }
        }

    }

    public void LevelUp()
    {
        Destroy(oldBackground);
        currentBackground.transform.position = new Vector3(0, 0, 6);
        ship.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y - 104, 0);
        ship.GetComponent<MovingObjectsBase>().move = false;

        alienMax += alienMax;
    }

}