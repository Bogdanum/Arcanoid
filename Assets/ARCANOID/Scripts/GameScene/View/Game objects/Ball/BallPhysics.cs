using UnityEngine;

public class BallPhysics : MonoBehaviour
{
     [SerializeField] private Rigidbody2D ballRigidbody;

     private float _velocity;

     public void SetVelocity(float velocity)
     {
          _velocity = velocity;
     }

     public void DisablePhysics()
     {
          ballRigidbody.simulated = false;
     }

     public void StartMovement(Vector2 velocityVector)
     {
          _velocity = velocityVector.magnitude;
          ballRigidbody.velocity = velocityVector;
          ballRigidbody.simulated = true;
     }

     private void FixedUpdate()
     {
          if (Mathf.Abs(ballRigidbody.velocity.magnitude - _velocity) > 0)
          {
               ballRigidbody.velocity = ballRigidbody.velocity.normalized * _velocity;
          }
     }
}
