using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollowing : MonoCache
{
    private Vector3 hitPoint;
    private Vector3 direction;
    private Ray ray;
    private Plane groundPlane;
    private Vector2 mouseScreenPos;
    public override void OnTick()
    {
        CalculateDirection();
    }
    private void CalculateDirection()
    {
        mouseScreenPos = Pointer.current.position.ReadValue();
        ray = Camera.main.ScreenPointToRay(mouseScreenPos);

        //Debug.Log("Mouse position: " + mouseScreenPos + "   Camera ray: " + ray);

        if (groundPlane.Raycast(ray, out float enter)) 
        {
            hitPoint = ray.GetPoint(enter);
            direction = hitPoint - transform.position;
            direction.y = 0f;
            Following(direction);
        }
    }
    private void Following(Vector3 direction) => transform.rotation = Quaternion.LookRotation(direction);
}