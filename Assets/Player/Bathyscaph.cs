using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bathyscaph : MonoBehaviour
{
	public event Action<GameObject, float> Damaged;
	public event Action Death;

	[SerializeField] private float _accelerationSpeed;
	[SerializeField] private float _diveSpeed;

	[SerializeField] private float _turnSpeed;
	[SerializeField] private float _lookAtSpeed;

	[SerializeField] private float _lookAtMax;
	[SerializeField] private float _lookAtMin;

	[SerializeField] private float _interactionDistance;

	[SerializeField] private float _sleepModeMaxTime;

	private bool _sleepMode;
	private float _sleepModeTime;

	private float _oxygen;
	private float _safety;

	private bool _isGrabbing;
	private GrabInteractive _grabInteractive;

	private bool _isDragging;
	private Transform _puller;

	private float _turnValue;
	private float _lookAtValue;

	private Transform _cachedTransform;

	private Rigidbody _rigidbody;

	private void Start()
	{
		_cachedTransform = transform;
		_rigidbody = GetComponent<Rigidbody>();

		_oxygen = 100;
		_safety = 100;

		_isGrabbing = false;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Interact"))
		{
			if (_isGrabbing)
			{
				_isGrabbing = false;
				_grabInteractive.StopInteraction(this);
			}
			else
			{
				Ray ray = new(_cachedTransform.position, _cachedTransform.forward);
				bool hit = Physics.Raycast(ray, out RaycastHit raycastHit, _interactionDistance);

				if (hit)
				{
					IInteractable interactive = raycastHit.collider.GetComponent<IInteractable>();

					if (interactive != null)
					{
						interactive.Interact(this);
					}
				}
			}
		}

		if(_oxygen > 0)
		{
			_oxygen = Mathf.MoveTowards(_oxygen, 0, Time.deltaTime / 4);
		}

		if (_sleepMode)
		{
			if(_sleepModeTime > 0)
			{
				if(_sleepModeTime - Time.deltaTime > 0)
				{
					_sleepModeTime -= Time.deltaTime;
				}
				else
				{
					_sleepModeTime = 0;
				}
			}
			else
			{
				_rigidbody.useGravity = false;
				_sleepMode = false;
			}
		}

		Debug.Log($"Oxygen: {_oxygen} Safety: {_safety} Speed: {_rigidbody.velocity.magnitude}");
	}

	private void CalcRotation()
	{
		_lookAtValue -= Input.GetAxis("Vertical") * _lookAtSpeed;
		_lookAtValue = Mathf.Clamp(_lookAtValue, _lookAtMin, _lookAtMax);

		_turnValue += Input.GetAxis("Horizontal") * _turnSpeed;

		_rigidbody.MoveRotation(Quaternion.Euler(_lookAtValue, _turnValue, 0));
	}

	private void CalcMovement()
	{
		if (Input.GetButton("Fire1"))
		{
			_rigidbody.AddForce(_cachedTransform.forward * _accelerationSpeed, ForceMode.Acceleration);
		}

		if (Input.GetButton("Fire2"))
		{
			_rigidbody.AddForce(-_cachedTransform.forward * _accelerationSpeed, ForceMode.Acceleration);
		}

		_rigidbody.velocity *= 0.98f;
	}

	private void FixedUpdate()
	{
		CalcRotation();
		CalcMovement();

		if (_isDragging)
		{
			_puller.position = Vector3.Lerp(_puller.position,
											_cachedTransform.position + _cachedTransform.forward * 2.5f,
											Time.deltaTime * 4);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		IContactable contactable = collision.gameObject.GetComponent<IContactable>();

		if (contactable != null)
		{
			contactable.Contact(this);
		}
	}

	private void OnDamaged(GameObject sender, float amount)
	{
		Transform senderTransform = sender.transform;

		if (senderTransform != null)
		{
			Vector3 directionFromSender = (_cachedTransform.position - senderTransform.position).normalized;

			_rigidbody.AddForce(directionFromSender * amount, ForceMode.Impulse);
		}


		_rigidbody.useGravity = true;

		_sleepMode = true;
		_sleepModeTime = _sleepModeMaxTime;
	}

	private void OnDeath()
	{

	}

	public void ApplyDamage(GameObject sender, float amount)
	{
		Debug.Log(sender);

		Damaged?.Invoke(sender, amount);
		OnDamaged(sender, amount);

		if (_safety - amount > 0)
		{
			_safety -= amount;
		}
		else
		{
			_safety = 0;

			Death?.Invoke();
			OnDeath();
		}
	}

	public void Grab(GrabInteractive grabInteractive)
	{
		_grabInteractive = grabInteractive;
		_isGrabbing = true;
	}

	public void Drag(Transform puller)
	{
		_isDragging = true;
		_puller = puller;
	}

	public void Drop()
	{
		_isDragging = false;
		_puller = null;
	}
}
