using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ManaManager : MonoBehaviour
{
    [Title("Setup")]
    [SerializeField] private List<UnitSetup> units;
    [SerializeField] private List<UnitsDisplay> unitsDisplay;

    [Title("Mana")]
    [SerializeField] private ManaCounter mana;

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
        Manager.Instance.OnEndGame += End;

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

    private void Update()
    {
        if (_ended) return;

        //TODO:Separar a UI
        float amount = mana.currentMana / mana.maxMana;
        manaDisplay.fillAmount = amount;
        manaText.text = ((int)(amount * 10)).ToString();

        if (!_isSelected) manaSpent.fillAmount = 0;

        Inputing();
        AllowTouch();

    }

    //TODO:Script para identificar o raycast
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
                Spawn();
            }
        }

    }

    private void Spawn()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (unitsDisplay[i].active && units[i].manaCost <= mana.currentMana)
            {
                Instantiate(units[i].unityPFB, feedback.transform.position, Quaternion.identity);

                mana.UseMana(units[i].manaCost);

                unitsDisplay[i].rectTransform.DOMoveY(unitsDisplay[i].height, duration).SetEase(ease);
                unitsDisplay[i].active = false;
                units[i].counter.StartCooldown();

                feedback.SetActive(false);
                _isSelected = false;
            }
        }
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

                manaSpent.fillAmount = units[unitsDisplay.IndexOf(u)].manaCost / mana.maxMana;
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
            bool notEnoughMana = units[i].counter.onCooldown ? true : units[i].manaCost > mana.currentMana;

            unitsDisplay[i].charging.gameObject.SetActive(notEnoughMana);
            unitsDisplay[i].button.interactable = !notEnoughMana;

            if (units[i].counter.onCooldown)
                unitsDisplay[i].charging.fillAmount = units[i].counter.currentCooldown / units[i].counter.maxCooldown;
            else if (notEnoughMana)
                unitsDisplay[i].charging.fillAmount = mana.currentMana / units[i].manaCost;

        }
    }

    private void End()
    {
        _ended = true;
    }

}


[System.Serializable]
public class UnitsDisplay
{
    public Button button;
    public Image charging;
    public TextMeshProUGUI cost;
    public bool active;
    [HideInInspector]
    public float height;
    [HideInInspector]
    public RectTransform rectTransform;
}


