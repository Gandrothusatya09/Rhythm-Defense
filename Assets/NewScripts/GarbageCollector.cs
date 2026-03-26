using UnityEngine;
using TMPro;

public class GarbageCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Garbage"))
        {
            Debug.Log("Garbage Collected");

            FindObjectOfType<ConditionBar>().AddCondition();

            Destroy(other.gameObject); 
        }
    }
}
