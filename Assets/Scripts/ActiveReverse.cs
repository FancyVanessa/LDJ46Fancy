using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveReverse : MonoBehaviour
{
    public void ActiveOnOff()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
}
