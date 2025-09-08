using System.IO.Enumeration;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "HealthSO", menuName = "Scriptable Objects/HealthSO")]
public class HealthSO : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }

    public void LoseHealth()
    {
        Debug.Log("Health Lost");
        currentHealth -= 10;
        Debug.Log(currentHealth);
    }
}
