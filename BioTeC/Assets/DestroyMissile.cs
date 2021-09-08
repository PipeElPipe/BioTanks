using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMissile : MonoBehaviour
{
    [SerializeField] float lasting = 20;

    void OnEnable()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lasting);
        Destroy(this.gameObject);
    }

}
