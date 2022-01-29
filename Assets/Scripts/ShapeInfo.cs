using UnityEngine;

public class ShapeInfo : MonoBehaviour
{
    public ShapeColor MyColor { private set; get; }

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        MyColor = (ShapeColor)Random.Range(0, 4);
        if (MyColor == ShapeColor.Red) sr.color = Color.red;
        else if (MyColor == ShapeColor.Blue) sr.color = Color.blue;
        else if (MyColor == ShapeColor.Green) sr.color = Color.green;
        else sr.color = Color.yellow;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 3f, ForceMode2D.Impulse);
    }

    public enum ShapeColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }
}
