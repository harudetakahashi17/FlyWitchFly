using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WaitSec : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnComplete;
    public void Wait(float seconds)
    {
        StartCoroutine(IeWaitSec(seconds));
    }
    private IEnumerator IeWaitSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (OnComplete != null)
        {
            OnComplete.Invoke();
        }
    }
}