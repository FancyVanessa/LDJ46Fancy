using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecharge : MonoBehaviour
{
    public Vector3 PlayerRechargeAdd;
    Vector3 PlayerFirePoint;
    Vector3 PlayerRechargePoint;
    float T = 0;
    public float SpeedMultiplier = 15f;

    void Start()
    {
        PlayerFirePoint = transform.position;
        PlayerRechargePoint = PlayerFirePoint + PlayerRechargeAdd;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2"))
            T += Time.deltaTime * SpeedMultiplier;
        else
            T -= Time.deltaTime * SpeedMultiplier;

        T = Mathf.Clamp(T, 0, 1);
        transform.position = Vector3.Lerp(PlayerFirePoint, PlayerRechargePoint, T);
    }
}
