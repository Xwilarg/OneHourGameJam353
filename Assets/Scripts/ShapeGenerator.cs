using System.Collections;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour
{
    [SerializeField]
    private int _nbGenerated;

    [SerializeField]
    private GameObject[] _prefabs;

    [SerializeField]
    private int _xBound, _waitTime;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (_nbGenerated > 0)
        {
            Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], new Vector2(Random.Range(-_xBound, _xBound), transform.position.y), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            yield return new WaitForSeconds(_waitTime);
            _nbGenerated--;
        }
    }
}
