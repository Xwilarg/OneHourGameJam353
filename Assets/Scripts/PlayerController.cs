using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private BoxCollider2D _bc;
    float _xDir;

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
                var orColor = _targetColor;
                while (orColor == _targetColor)
                {
                    SetTargetColor();
                }
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
        if (_targetColor == ShapeInfo.ShapeColor.Red) _sr.color = Color.red;
        else if (_targetColor == ShapeInfo.ShapeColor.Blue) _sr.color = Color.blue;
        else if (_targetColor == ShapeInfo.ShapeColor.Green) _sr.color = Color.green;
        else _sr.color = Color.yellow;
    }

    private ShapeInfo.ShapeColor _targetColor;
}
