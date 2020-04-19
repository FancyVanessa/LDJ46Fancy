using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;

    public float BarSize = 1f;

    float CurrentHealth = 1f;
    float ComingHealth = 1f;
    float t = 1f;
    float SpeedMultiplier = 5f;

    void Start()
    {
        bar = transform.Find("Bar");
    }

    private void Update()
    {
        t += Time.deltaTime * SpeedMultiplier;
        BarSize = Mathf.Lerp(CurrentHealth, ComingHealth, t);

        bar.localScale = new Vector3(BarSize, 1f);
    }

    public void SetSize(float Size)
    {
        CurrentHealth = BarSize;
        ComingHealth = Size;
        t = 0f;
        ComingHealth = Mathf.Clamp(ComingHealth, 0f, 1f);
    }
}
