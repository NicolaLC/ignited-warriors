using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        Transform transform1 = transform;
        Vector3 position = target.position;

        Vector3 targetPosition = new Vector3(position.x, transform1.position.y, position.z);
        transform.position = Vector3.Lerp(transform1.position, targetPosition, Time.deltaTime * 5f);
    }
}