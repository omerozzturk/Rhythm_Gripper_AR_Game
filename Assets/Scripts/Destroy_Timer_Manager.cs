using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Timer_Manager : MonoBehaviour
{
    public void Destroy_Timer_Init()
    {
        StartCoroutine(Destroy_Timer_Core());
    }

    private IEnumerator Destroy_Timer_Core()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
