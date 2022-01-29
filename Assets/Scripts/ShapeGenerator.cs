using System.Collections;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    public static ShapeGenerator S;

    private void Awake()
    {
        S = this;
    }

    [SerializeField]
    private GameObject[] _prefabs;

    [SerializeField]
    private int _xBound;

    private float _waitTime = 1f;

    public void ReduceWaitTime()
    {
        if (_waitTime > .5f)
        {
            _waitTime -= .5f;
        }
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], new Vector2(Random.Range(-_xBound, _xBound), transform.position.y), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
