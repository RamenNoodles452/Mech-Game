using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class WalkerMovement : MonoBehaviour {
	
	public Transform lowerBody;
	
	public float runSpeed = 9.0f;
	public float walkSpeed = 6.0f;
	
	private bool isControllable = true;
	
	float rotateSpeed = 500.0f;
	public float decelerationSmoothing = 10.0f;
	public float accelerationSmoothing = 2.0f;
	public float accelerationModifier = 0.0f;
	public float decelerationModifier = 0.0f;
	
	// Is the user pressing any keys?
	private bool isMoving = false;
	// The current move direction in x-z
	private Vector3 moveDirection = Vector3.zero;
	// The current x-z move speed
	private float moveSpeed = 0.0f;
	// The current vertical speed
	private float verticalSpeed = 0.0f;
	
	// The last collision flags returned from controller.Move
	private CollisionFlags collisionFlags; 
	
	public enum CharacterState
	{
		Idle = 0,
		Walking = 1,
		Running = 2,
		Jumping = 3,
		Attacking = 4,
		Dodging = 5,
		Blocking = 6
	};
	private CharacterState _characterState;
	
	// Use this for initialization
	void Start () 
	{
		moveDirection = lowerBody.TransformDirection(Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isControllable)
		{
			// kill all inputs if not controllable.
			Input.ResetInputAxes();
		}
		
		UpdateSmoothedMovementDirection();
		
		//calculate the movement
		Vector3 movement = moveDirection * moveSpeed + new Vector3(0.0f, verticalSpeed, 0.0f);
		movement *= Time.deltaTime;
		
		
		// move the controller
		CharacterController controller = GetComponent<CharacterController>();
		collisionFlags = controller.Move(movement);
		
		
		//rotate the character to the move direction
		if(_characterState == CharacterState.Idle || _characterState == CharacterState.Walking || _characterState == CharacterState.Running)
		{
			lowerBody.rotation = Quaternion.LookRotation(moveDirection);
		}
	}
	
	void UpdateSmoothedMovementDirection()
	{
		Transform cameraTransform = Camera.main.transform;
		
		//apply gravity
		if(IsGrounded())
		{
			verticalSpeed = 0.0f;	
		}
		else
		{
			verticalSpeed -= 10.0f * Time.deltaTime;
		}
		
		//forward vector relative to the camera along the xz plane
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		
		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
		
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");
		
		bool wasMoving = isMoving;
		isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;
		
		// target direction relative to the camera
		Vector3 targetDirection = h * right + v * forward;
		
		//we store speed and direction seperately
		// so that when the character stands still we still have a valid forward direction
		// move direction is always normalized, and we only update it if there is user input
		if(targetDirection != Vector3.zero)
		{
			// otherwise smoothly turn towards target direction
			moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
			moveDirection = moveDirection.normalized;
		}
		float curSmooth = 0;
		//smooth the speed based on the current target direction
		if(h != 0 || v != 0)
		{
			curSmooth = (accelerationSmoothing + accelerationModifier) * Time.deltaTime;
		}
		else
		{
			curSmooth = (decelerationSmoothing + decelerationModifier) * Time.deltaTime;
		}
		
		//chose target speed
		//* We want to support analog input but make sure you can't walk faster diagonally than just forward or sideways
		float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);
		
		_characterState = CharacterState.Idle;
				
		// pick speed modifier
		if(Input.GetKey(KeyCode.LeftShift))
		{
			targetSpeed *= runSpeed;
			_characterState = CharacterState.Running;
		}
		else if(isMoving)
		{
			targetSpeed *= walkSpeed;
			_characterState = CharacterState.Walking;
		}
		
		moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
	}
	
	bool IsGrounded () 
	{
		return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
	}
}
