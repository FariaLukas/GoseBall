using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Health _health;
    public Image lifeDisplay;
    public Color hitColor = Color.white;
    public Color lifeColor = Color.red;

    private void OnEnable()
    {
        _health = GetComponent<Health>();

        lifeDisplay.fillAmount = _health.currentLife / _health.maxLife;
        lifeDisplay.color = lifeColor;

        _health.onTakeDamage += OnTakeDamage;
        _health.onHeal += UpdateUI;
    }

    private void OnTakeDamage()
    {
        lifeDisplay.DOKill();

        UpdateUI();

        lifeDisplay.DOColor(hitColor, 0.05f).SetLoops(2, LoopType.Yoyo).
        OnComplete(() => UpdateUI());
    }

    private void UpdateUI()
    {
        lifeDisplay.DOFillAmount((float)_health.currentLife / _health.maxLife, 0.1f);
        lifeDisplay.color = lifeColor;
    }

    private void OnDisable()
    {
        _health.onTakeDamage -= OnTakeDamage;
        _health.onHeal -= UpdateUI;
    }
}
