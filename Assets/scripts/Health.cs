using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider FillBar;
    public Player health;

    private void Start()
    {
        SetMaxHealth(health.health);
    }
    private void Update()
    {
        SetHealth(health.health);
    }
    public void SetMaxHealth(int health)
    {
        FillBar.maxValue = health;
        FillBar.value = health;
    }
    public void SetHealth(int health)
    {
        FillBar.value = health;
    }
}
