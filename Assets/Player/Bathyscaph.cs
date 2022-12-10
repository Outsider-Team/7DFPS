using System;
using UnityEngine;

public class Bathyscaph : MonoBehaviour
{
	[SerializeField] private float _accelerationSpeed;
	[SerializeField] private float _diveSpeed;

	[SerializeField] private float _turnSpeed;
	[SerializeField] private float _lookAtSpeed;

	[SerializeField] private float _lookAtMax;
	[SerializeField] private float _lookAtMin;

	[SerializeField] private float _interactionDistance;

	private float _oxygen;
	private float _safety;

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
	}

	private void Update()
	{
		CalcRotation();
		CalcMovement();

		if (Input.GetButtonDown("Interact"))
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

		if(_oxygen > 0)
		{
			_oxygen = Mathf.MoveTowards(_oxygen, 0, Time.deltaTime / 4);
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

		if (Input.GetButton("Dive"))
		{
			_rigidbody.AddForce(Vector3.down * _diveSpeed, ForceMode.Acceleration);
		}

		_rigidbody.velocity *= 0.98f;
	}

	private void OnCollisionEnter(Collision collision)
	{
		IContactable contactable = collision.gameObject.GetComponent<IContactable>();

		if (contactable != null)
		{
			contactable.Contact(this);
		}
	}

	public void ApplyDamage(GameObject sender, float amount)
	{
		Debug.Log(sender);

		Transform senderTransform = sender.transform;
		
		if (senderTransform != null)
		{
			Vector3 directionFromSender = (_cachedTransform.position - senderTransform.position).normalized;
		
			_rigidbody.AddForce(directionFromSender * amount, ForceMode.Impulse);
		}

		if(_safety - amount > 0)
		{
			_safety -= amount;
		}
		else
		{
			_safety = 0;
		}
	}
}
