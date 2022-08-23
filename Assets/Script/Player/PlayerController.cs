using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PlayerBase
{
    public List<UnitsDisplay> unitsDisplay;

    [Title("UI")]
    [SerializeField] private Image manaDisplay;
    [SerializeField] private Image manaSpent;
    [SerializeField] private TextMeshProUGUI manaText;

    [Title("FeedBack")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject feedback;

    [Title("Animation")]
    [SerializeField] private Ease ease = Ease.OutBack;
    [SerializeField] private float duration = .2f;
    [SerializeField] private float endPos = -50;

    private bool _isSelected;
    private bool _ended;

    private void Start()
    {

        for (int i = 0; i < units.Count; i++)
        {
            unitsDisplay[i].cost.text = units[i].manaCost.ToString();
            if (unitsDisplay[i].button.TryGetComponent(out RectTransform rect))
            {
                unitsDisplay[i].height = rect.position.y;
                unitsDisplay[i].rectTransform = rect;

            }
        }
    }
    protected override void Update()
    {
        base.Update();

        if (_ended) return;

        float amount = manaCounter.currentMana / manaCounter.maxMana;
        manaDisplay.fillAmount = amount;
        manaText.text = ((int)(amount * 10)).ToString();

        if (!_isSelected) manaSpent.fillAmount = 0;

        Inputing();
        AllowTouch();
    }

    private void Inputing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool insideBoard = Physics.Raycast(ray, out hit, Mathf.Infinity, mask) &&
         hit.collider.tag != "Enemy" && hit.collider.tag != "Ally";

        feedback.SetActive(insideBoard && _isSelected);

        if (insideBoard && _isSelected)
        {
            feedback.transform.position = hit.point;
            
            if (Input.GetMouseButtonDown(0))
            {
                foreach (var u in unitsDisplay)
                {
                    if (!u.active) continue;
                    Spawn(unitsDisplay.IndexOf(u), feedback.transform.position);
                    break;
                }
            }
        }

    }

    protected override void Spawn(int index, Vector3 position)
    {
        if (!CanSpawn(index)) return;
        base.Spawn(index, position);

        unitsDisplay[index].rectTransform.DOMoveY(unitsDisplay[index].height, duration).SetEase(ease);
        unitsDisplay[index].active = false;

        feedback.SetActive(false);
        _isSelected = false;
    }

    public void Select(Button button)
    {
        foreach (UnitsDisplay u in unitsDisplay)
        {
            if (u.button == button)
            {
                u.active = !u.active;
                _isSelected = u.active;

                if (!u.active)
                    u.rectTransform.DOMoveY(u.height, duration).SetEase(ease);
                else
                    u.rectTransform.DOLocalMoveY(u.rectTransform.localPosition.y + endPos, duration).SetEase(ease);

                manaSpent.fillAmount = units[unitsDisplay.IndexOf(u)].manaCost / manaCounter.maxMana;
            }
            else
            {
                if (u.active)
                    u.rectTransform.DOMoveY(u.height, duration).SetEase(ease);
                u.active = false;
            }
        }

    }

    private void AllowTouch()
    {
        for (int i = 0; i < units.Count; i++)
        {
            bool notEnoughMana = units[i].counter.onCooldown ? true : units[i].manaCost > manaCounter.currentMana;

            unitsDisplay[i].charging.gameObject.SetActive(notEnoughMana);
            unitsDisplay[i].button.interactable = !notEnoughMana;

            if (units[i].counter.onCooldown)
                unitsDisplay[i].charging.fillAmount = units[i].counter.currentCooldown / units[i].counter.maxCooldown;
            else if (notEnoughMana)
                unitsDisplay[i].charging.fillAmount = manaCounter.currentMana / units[i].manaCost;

        }
    }

    protected override void End()
    {
        _ended = true;
    }
}
