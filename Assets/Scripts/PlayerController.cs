using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private SpriteRenderer[] _addColors;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private BoxCollider2D _bc;
    private float _xDir;
    private int _score;

    private Color _oldcolor;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _bc = GetComponent<BoxCollider2D>();
        SetTargetColor();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_xDir * 10f, _rb.velocity.y);
        if (_rb.velocity.x > 0f)
        {
            _sr.flipX = true;
        }
        else if (_rb.velocity.x < 0f)
        {
            _sr.flipX = false;
        }
    }

    private void Update()
    {
        _xDir = Input.GetAxis("Horizontal");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Shape"))
        {
            var shape = collision.collider.GetComponent<ShapeInfo>();
            if (shape.MyColor != _targetColor)
            {
                _sr.enabled = false;
                _bc.enabled = false;
                StartCoroutine(WaitAndRestart());
            }
            else
            {
                Destroy(collision.collider.gameObject);
                _sr.color = _oldcolor;
                var orColor = _targetColor;
                while (orColor == _targetColor)
                {
                    SetTargetColor();
                }
                _score++;
                _scoreText.text = $"Score: {_score}";
                ShapeGenerator.S.ReduceWaitTime();
            }
        }
    }

    private IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    private void SetTargetColor()
    {
        _targetColor = (ShapeInfo.ShapeColor)Random.Range(0, 4);
        Color c;
        if (_targetColor == ShapeInfo.ShapeColor.Red) c = Color.red;
        else if (_targetColor == ShapeInfo.ShapeColor.Blue) c = Color.blue;
        else if (_targetColor == ShapeInfo.ShapeColor.Green) c = Color.green;
        else c = Color.yellow;
        foreach (var o in _addColors)
        {
            o.color = c;
        }
        _oldcolor = c;
    }

    private ShapeInfo.ShapeColor _targetColor;
}
