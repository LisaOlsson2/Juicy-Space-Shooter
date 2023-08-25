using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    // this is pretty easy to understand, i don't think i need to explain it


    SpriteRenderer spriteRenderer;
    Color color;
    public float colorSpeed;

    int step = 1; // represents the current step. Each step is written below
    // increasingG -> decreasingR -> increasingB -> decreasingG -> increasingR -> decreasingB

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;

    }

    // Update is called once per frame
    void Update()
    {

        spriteRenderer.color = color;
        if (step == 1)
        {
            color.g += Time.deltaTime * colorSpeed;
            if (color.g >= 1)
            {
                step++;
            }
        }
        if (step == 2)
        {
            color.r -= Time.deltaTime * colorSpeed;
            if (color.r <= 0)
            {
                step++;
            }
        }
        if (step == 3)
        {
            color.b += Time.deltaTime * colorSpeed;
            if (color.b >= 1)
            {
                step++;
            }
        }
        if (step == 4)
        {
            color.g -= Time.deltaTime * colorSpeed;
            if (color.g <= 0)
            {
                step++;
            }
        }
        if (step == 5)
        {
            color.r += Time.deltaTime * colorSpeed;
            if (color.r >= 1)
            {
                step++;
            }
        }
        if (step == 6)
        {
            color.b -= Time.deltaTime * colorSpeed;
            if (color.b <= 0)
            {
                step = 1;
            }
        }

    }
}
