using UnityEngine;

public class YSort : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private int offset = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + offset;
    }
}