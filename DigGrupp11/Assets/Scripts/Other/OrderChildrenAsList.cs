using UnityEngine;

public class OrderChildrenAsList : MonoBehaviour
{
    [SerializeField] float childOffset;
    void OnTransformChildrenChanged()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform childTransform = transform.GetChild(i).GetComponent<RectTransform>();
            childTransform.localPosition = new Vector3(0, -i * childOffset, 0);
        }
    }
}
