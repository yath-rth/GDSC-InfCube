using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    public GameObject[] objects;
    public float openTime;
    public float waitTime;

    public Vector3 size = Vector3.one;

    WaitForSecondsRealtime wait;

    private void OnEnable()
    {
        wait = new WaitForSecondsRealtime(waitTime);

        StartCoroutine(opened());
    }

    IEnumerator opened()
    {
        foreach (GameObject obj in objects)
        {
            obj.transform.localScale = Vector3.zero;
        }

        foreach (GameObject obj in objects)
        {
            obj.transform.DOScale(size, openTime).SetEase(Ease.OutBounce).SetUpdate(true);
            yield return wait;
        }
    }
}
