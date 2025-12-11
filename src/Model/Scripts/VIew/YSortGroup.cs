using UnityEngine;
using UnityEngine.Rendering;

public class YSortGroup : MonoBehaviour
{
    private SortingGroup sortingGroup;

    [SerializeField] private int offset = 0;

    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
    }

    private void LateUpdate()
    {
        int order = Mathf.RoundToInt(-transform.position.y * 100) + offset;
        sortingGroup.sortingOrder = order;
    }
}