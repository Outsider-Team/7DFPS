using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Puller : GrabInteractive
{
	private Bathyscaph _bathyscaph;
	private bool _isDragging;

	private Rigidbody _rigidbody;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void OnDestroy()
	{
		if (_isDragging)
		{
			_bathyscaph.Drop();
		}
	}

	protected override void OnInteract(Bathyscaph bathyscaph)
	{
		base.OnInteract(bathyscaph);

		_bathyscaph = bathyscaph;

		_rigidbody.useGravity = false;

		bathyscaph.Drag(transform);

		_isDragging = true;
	}

	protected override void OnStopInteraction(Bathyscaph bathyscaph)
	{
		bathyscaph.Drop();
		_rigidbody.useGravity = true;
		_isDragging = false;
	}
}
