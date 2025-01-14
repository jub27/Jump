using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        transform.parent = Character.Instance.transform;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(0, 10.77f, 0);
        GameManager.Instance.Goal();
    }
}
