using UnityEngine;
using UnityEngine.Events;

public class PlayerCloneController : MonoBehaviour
{
    public UnityAction<PlayerCloneController> callback;

    [SerializeField] private float fadeSpeed = 4f;
    private float _animTimer;
    private Animator _animator;
    private SpriteRenderer _sr;
    private bool _isFade;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void Release()
    {
        _animTimer = 0f;
        Color newColor = _sr.color;
        newColor.a = 1f;
        _sr.color = newColor;
        _isFade = false;

        int randomInteger = Random.Range(1, 4);
        _animator.SetInteger("AttackNumber", randomInteger);
        _animator.SetBool("Attack", true);
    }

    private void Update()
    {
        if (_isFade == false)
        {
            _animTimer += Time.deltaTime;
            if (_animTimer > 0.5f)
            {
                _animator.SetBool("Attack", false);
                _isFade = true;
            }
        }
        else
        {
            Color currentColor = _sr.color;
            currentColor.a = Mathf.MoveTowards(currentColor.a, 0, fadeSpeed * Time.deltaTime);
            _sr.color = currentColor;

            if (currentColor.a <= 0)
            {
                callback?.Invoke(this);
            }
        }
    }
}