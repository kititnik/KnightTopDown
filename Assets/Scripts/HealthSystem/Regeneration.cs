using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Regeneration : MonoBehaviour
{
    [SerializeField] private float regenerationTime;
    [SerializeField] private GameObject regeneratingObject;
    [SerializeField] private UnityEvent onStartedRegeneration;
    [SerializeField] private UnityEvent onRegenerated;
    [SerializeField] private int orderInLayer;

    public void StartRegeneration()
    {
        onStartedRegeneration?.Invoke();
        StartCoroutine(RegenerationCor());
    }

    private IEnumerator RegenerationCor()
    {
        yield return new WaitForSeconds(regenerationTime);
        var go = ExtentionMethods.InstantiateWithSortingOrder(regeneratingObject, transform, orderInLayer);
        go.GetComponent<Health>().OnBroken.AddListener(StartRegeneration);
        onRegenerated?.Invoke();
    }
}
