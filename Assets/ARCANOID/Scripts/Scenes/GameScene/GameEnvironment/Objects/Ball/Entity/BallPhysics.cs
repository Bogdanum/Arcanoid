using UnityEngine;

public class BallPhysics : MonoBehaviour
{
     [SerializeField] private Rigidbody2D ballRigidbody;
     [SerializeField] private BallPhysicsSettings ballPhysicsSettings;
     private ReboundDirectionCalculator _reboundDirectionCalculator;
     private float _velocity;
     public bool IsMoving { get; private set; }

     public void SetVelocity(float velocity)
     {
          _velocity = velocity;
          _reboundDirectionCalculator = new ReboundDirectionCalculator(ballRigidbody, ballPhysicsSettings);
          _reboundDirectionCalculator.UpdateVelocity(velocity);
     }

     public void DisablePhysics()
     {
          ballRigidbody.simulated = false;
     }

     public void EnablePhysics()
     {
          ballRigidbody.simulated = true;
     }

     public void OnDespawned() => IsMoving = false;

     public void StartMovement(Vector2 velocityVector)
     {
          IsMoving = true;
          _velocity = velocityVector.magnitude;
          _reboundDirectionCalculator.UpdateVelocity(_velocity);
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

     private void OnCollisionEnter2D(Collision2D col) => _reboundDirectionCalculator?.OnCollisionEnter2D(col);
}
