using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarContoller : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider funSlider;
    [SerializeField] private Gradient grad;
    [SerializeField] private Image healthFill;
    [SerializeField] private Image hungerFill;
    [SerializeField] private Image funFill;

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        healthFill.color = grad.Evaluate(healthSlider.normalizedValue);
    }

    public void SetHunger(float hunger)
    {
        hungerSlider.value = hunger;
        hungerFill.color = grad.Evaluate(hungerSlider.normalizedValue);
    }

    public void SetFun(float fun)
    {
        funSlider.value = fun;
        funFill.color = grad.Evaluate(funSlider.normalizedValue);
    }

    // TODO: Test Code Remove Later
    private void Update()
    {

    }

}
