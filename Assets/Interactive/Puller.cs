using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Puller : GrabInteractive
{
	private Rigidbody _rigidbody;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	protected override void OnInteract(Bathyscaph bathyscaph)
	{
		base.OnInteract(bathyscaph);

		_rigidbody.useGravity = false;

		bathyscaph.Drag(transform);

	}

	protected override void OnStopInteraction(Bathyscaph bathyscaph)
	{
		bathyscaph.Drop();
		_rigidbody.useGravity = true;
	}
}
