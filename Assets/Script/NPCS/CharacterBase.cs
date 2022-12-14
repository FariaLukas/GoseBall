using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;
public class CharacterBase : MonoBehaviour, IListener
{
    [Title("Events")]
    [SerializeField] private CustomEvent onEndGame;
    [SerializeField] private CustomEvent onStunStarted, onStunFinished;

    [Title("Setup")]
    public SOAtributes atributes;
    public Health health;

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
    protected BTParallelSelector parallel = new BTParallelSelector();
    protected BTSequence movement = new BTSequence();
    protected BTSelector selector = new BTSelector();

    protected GameObject _ball;
    private BehaviorTree _behavior;
    private Collider _collider;
    private Text text;

    protected virtual void Init()
    {
        _behavior = gameObject.AddComponent<BehaviorTree>();

        _collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();

        if (!health) health = GetComponent<Health>();

        health.SetupLife(atributes.life);
        health.onDie += Die;

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

    public virtual void HoldBall(GameObject ball)
    {
        haveTheBall = true;
        _ball = ball;
        _ball.transform.parent = transform;
        _ball.transform.position = ballPosition.position;
        charRigidbody.isKinematic = true;
    }

    public virtual void Attack(GameObject enemy)
    {
        transform.DOLookAt(enemy.transform.position, .1f);
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

        health.onDie -= Die;

        DOTween.KillAll();

        _behavior.StopAll();
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
            health.TakeDamage(1);
        }
    }

    private void EndGame()
    {
        _behavior.StopAll();
        AnimationManager.Instance.SetBool(animator, "Run_", false);
        AnimationManager.Instance.SetTrigger(animator, "Idle");
    }

    private void OnEnable()
    {
        RegisterAsListener();
        Init();
    }

    private void OnDisable()
    {
        UnregisterAsListener();
    }

    public void RegisterAsListener()
    {
        onEndGame?.RegisterListener(this);
        onStunStarted.RegisterListener(this);
        onStunFinished.RegisterListener(this);
    }

    public void UnregisterAsListener()
    {
        onEndGame?.UnregisterListener(this);
        onStunStarted.UnregisterListener(this);
        onStunFinished.UnregisterListener(this);
    }

    public void OnEventRaised(CustomEvent customEvent, object param)
    {
        if (customEvent.Equals(onEndGame))
            EndGame();

        if (customEvent.Equals(onStunStarted))
            Stun((string)param);

        if (customEvent.Equals(onStunFinished))
            EndStun((string)param);
    }

}
