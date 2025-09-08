using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DangerZone : MonoBehaviour
{
    private bool inDanger = false;

    public UnityEvent OnDanger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            inDanger = true;
            StartCoroutine(LoseHealth());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inDanger = false;
        }
    }

    IEnumerator LoseHealth()
    {
        OnDanger.Invoke();
        while (inDanger)
        {
            yield return new WaitForSeconds(2);
            OnDanger.Invoke();
        }
    }
}
