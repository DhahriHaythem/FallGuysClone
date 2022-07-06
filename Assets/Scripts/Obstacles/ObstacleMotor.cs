using UnityEngine;

public class ObstacleMotor: MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float HitForce;

    void Update()
    {
        transform.Rotate(direction * rotationSpeed * Time.deltaTime);
    }
    public float GetHitForce()
    {
        return HitForce;
    }

}
