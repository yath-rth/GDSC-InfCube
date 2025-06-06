using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class button : MonoBehaviour
{
    public void OnClick()
    {
        transform.localScale = Vector3.one;
        transform.DOPunchScale(new Vector3(.15f, 0.15f, 0.15f), 0.1f).SetUpdate(true);
    }
}
