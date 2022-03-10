using UnityEngine;

public class BallPhysics : MonoBehaviour
{
     [SerializeField] private Rigidbody2D ballRigidbody;

     private bool _active;
     private float _velocity;
     public bool IsMoving { get; private set; }

     public void SetVelocity(float velocity)
     {
          _velocity = velocity;
     }

     public void DisablePhysics()
     {
          ballRigidbody.simulated = false;
     }

     public void EnablePhysics()
     {
          ballRigidbody.simulated = true;
     }

     public void StartMovement(Vector2 velocityVector)
     {
          IsMoving = true;
          _velocity = velocityVector.magnitude;
          ballRigidbody.velocity = velocityVector;
          ballRigidbody.simulated = true;
     }

     private void Update()
     {
          if (Mathf.Abs(ballRigidbody.velocity.magnitude - _velocity) > 0)
          {
               ballRigidbody.velocity = ballRigidbody.velocity.normalized * _velocity;
          }
     }
}
