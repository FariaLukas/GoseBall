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
    public List<UnitSetup> units;
    public List<UnitsDisplay> unitsDisplay;

    [Title("Mana")]
    public float maxMana;
    public Image manaDisplay;
    public Image manaSpent;
    public TextMeshProUGUI manaText;

    [Title("FeedBack")]
    public LayerMask mask;
    public GameObject feedback;

    [Title("Animation")]
    public Ease ease = Ease.OutBack;
    public float duration = .2f;
    public float endPos = -50;

    private float _currentMana;
    private bool isSelected;
    private bool ended;
    public bool can;

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
        if (ended) return;
        if (_currentMana > maxMana)
        {
            _currentMana = maxMana;
            return;
        }
        float amount = _currentMana / maxMana;

        manaDisplay.fillAmount = amount;
        manaText.text = ((int)(amount * 10)).ToString();

        _currentMana += Time.deltaTime;
        if (!isSelected) manaSpent.fillAmount = 0;

        Inputing();
        AllowTouch();

    }


    private void Inputing()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool insideBoard = Physics.Raycast(ray, out hit, Mathf.Infinity, mask) &&
         hit.collider.tag != "Enemy" && hit.collider.tag != "Ally";
        can = insideBoard && isSelected;
        if (insideBoard && isSelected)
        {
            feedback.SetActive(true);

            feedback.transform.position = hit.point;
            if (Input.GetMouseButtonDown(0))
            {
                Spawn();
                print("clicked");
            }
        }
        else
        {
            feedback.SetActive(false);
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (unitsDisplay[i].active && units[i].manaCost <= _currentMana)
            {
                Instantiate(units[i].unityPFB, feedback.transform.position, Quaternion.identity);

                _currentMana -= units[i].manaCost;

                unitsDisplay[i].rectTransform.DOMoveY(unitsDisplay[i].height, duration).SetEase(ease);
                unitsDisplay[i].active = false;

                feedback.SetActive(false);
                isSelected = false;
            }
        }
    }

    public void Select(Button button)
    {
        foreach (UnitsDisplay b in unitsDisplay)
        {
            if (b.button == button)
            {
                b.active = !b.active;
                isSelected = b.active;
                if (!b.active)
                    b.rectTransform.DOMoveY(b.height, duration).SetEase(ease);
                else
                    b.rectTransform.DOMoveY(b.rectTransform.position.y + endPos, duration).SetEase(ease);

            }
            else
            {
                if (b.active)
                    b.rectTransform.DOMoveY(b.height, duration).SetEase(ease);
                b.active = false;
            }
        }
        for (int i = 0; i < units.Count; i++)
        {
            if (unitsDisplay[i].active)
            {
                manaSpent.fillAmount = units[i].manaCost / maxMana;
            }
        }

    }

    private void AllowTouch()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].manaCost > _currentMana)
            {
                unitsDisplay[i].charging.gameObject.SetActive(true);
                unitsDisplay[i].charging.fillAmount = _currentMana / units[i].manaCost;
                unitsDisplay[i].button.interactable = false;
            }
            else
            {
                unitsDisplay[i].charging.gameObject.SetActive(false);
                unitsDisplay[i].button.interactable = true;
            }
        }
    }

    private void End()
    {
        ended = true;
    }

}

[System.Serializable]
public class UnitSetup
{
    public GameObject unityPFB;
    public float manaCost;
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


