using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazeWobbler : MonoBehaviour
{
    public float WobbleAmountX = 1f;
    public float WobbleAmountY = 1f;
    public float WobbleSpeed = 1f;
    float WobbleTime;
    Vector3 WobblePos;

    private void Start()
    {
        WobblePos = transform.position;
    }
    void Update()
    {
        WobbleTime += Time.deltaTime * WobbleSpeed;
        Vector3 Wobble = new Vector2(Mathf.Sin(WobbleTime * WobbleAmountX) / 2 * Mathf.PI, Mathf.Sin(WobbleTime) * WobbleAmountY / 2 * Mathf.PI);
        transform.position = WobblePos + Wobble;
    }
}
