using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    [SerializeField] HealthSO health;
    [SerializeField] Image healthBarImage;

    private void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (health == null ||  healthBarImage == null)
        {
            Debug.Log("Something is null");
        }

        healthBarImage.fillAmount = health.currentHealth / health.maxHealth;
        Debug.Log(healthBarImage.fillAmount);
    }
}
