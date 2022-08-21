using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
public class CharacterBase : MonoBehaviour
{
    public CharacterAtributes atributes;
    public Image lifeDisplay;
    public Color rangeColor = Color.yellow;
    public Color hitColor = Color.white;
    public Color lifeColor = Color.red;
    public Transform ballPosition;
    public string projectile;
    public bool haveTheBall;
    public GameObject stunPFB;
    public GameObject deathPFB;

    [HideInInspector]
    public Rigidbody charRigidbody;
    [HideInInspector]
    public Animator animator;
    [ReadOnly]
    [TextArea]
    public string currentState;

    [HideInInspector]
    public float _maxLife;

    protected BTSequence combat = new BTSequence();
    protected BTSelectorParalelo parallel = new BTSelectorParalelo();
    protected BTSequence movement = new BTSequence();
    protected BTSelector selector = new BTSelector();
    protected GameObject _ball;
    private BehaviorTree _behavior;
    private Collider _collider;
    private Text text;
    private void OnEnable()
    {
        Special.Instance.OnStun += Stun;
        Special.Instance.OnStunFinished += EndStun;
        Manager.Instance.OnEndGame += EndGame;
        Init();
    }

    protected virtual void Init()
    {
        _behavior = gameObject.AddComponent<BehaviorTree>();
        _maxLife = atributes.life;
        _collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        lifeDisplay.fillAmount = atributes.life / _maxLife;
        lifeDisplay.color = lifeColor;
        text = GetComponentInChildren<Text>();
        charRigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (text)
            text.text = currentState;
    }

    protected virtual void AddRoot()
    {
        selector.children.Add(movement);
        selector.children.Add(combat);

        _behavior.root = selector;
    }

    public virtual void TakeDamage(int damage)
    {
        atributes.life -= damage;
        lifeDisplay.DOFillAmount(atributes.life / _maxLife, 0.1f);
        lifeDisplay.color = lifeColor;
        lifeDisplay.DOColor(hitColor, 0.05f).SetLoops(2, LoopType.Yoyo).OnComplete(() => lifeDisplay.color = lifeColor);
        if (atributes.life <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int heal)
    {
        lifeDisplay.color = lifeColor;
        if (atributes.life < _maxLife)
            atributes.life += heal;
        lifeDisplay.DOFillAmount(atributes.life / _maxLife, 0.1f);
    }

    public virtual void HoldBall(GameObject ball)
    {
        haveTheBall = true;
        _ball = ball;
        _ball.transform.parent = transform;
        _ball.transform.position = ballPosition.position;
        charRigidbody.isKinematic = true;

        lifeDisplay.color = lifeColor;
    }

    protected virtual void Die()
    {

        if (haveTheBall)
        {
            if (_ball)
                _ball.transform.parent = null;
            Manager.Instance.DropBall();
            haveTheBall = false;
        }

        GameObject death = Instantiate(deathPFB, transform.position, deathPFB.transform.rotation);
        Destroy(death, 3);

        Manager.Instance.OnEndGame -= EndGame;
        Special.Instance.OnStun -= Stun;
        Special.Instance.OnStunFinished -= EndStun;

        DOTween.KillAll();

        _behavior.StopAll();
        Destroy(gameObject);
    }

    private void Stun(string tag)
    {
        if (atributes.allyLabel == tag)
        {
            AnimationManager.Instance.SetTrigger(animator, "Stun");
            _behavior.StopAll();
            stunPFB.SetActive(true);
            if (haveTheBall)
            {
                if (_ball)
                    _ball.transform.parent = null;

                charRigidbody.isKinematic = true;

                _collider.enabled = false;

                Manager.Instance.DropBall();
                haveTheBall = false;
                
                lifeDisplay.color = lifeColor;
            }
        }
    }

    private void EndStun(string tag)
    {
        if (atributes.allyLabel == tag)
        {
            AnimationManager.Instance.SetTrigger(animator, "Idle");
            charRigidbody.isKinematic = false;
            _collider.enabled = true;
            _behavior.Initialize();
            stunPFB.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(projectile))
        {
            Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = rangeColor;
        Gizmos.DrawWireSphere(transform.position, atributes.range);
    }

    private void EndGame()
    {
        _behavior.StopAll();
    }

}
