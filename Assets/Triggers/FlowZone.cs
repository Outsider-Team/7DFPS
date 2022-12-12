using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FlowZone : MonoBehaviour
{
	[SerializeField] private float _intensity;
	private Transform _cachedTransform;

	private void Start()
	{
		_cachedTransform = transform;
	}

	private void OnTriggerStay(Collider other)
	{
		Rigidbody rigidbody = other.GetComponent<Rigidbody>();

		if (rigidbody != null)
		{
			rigidbody.AddForce(_cachedTransform.forward * _intensity, ForceMode.Acceleration);
		}
	}
}
