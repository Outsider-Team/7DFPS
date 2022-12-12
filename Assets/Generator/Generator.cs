using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public event Action Launched;

	[SerializeField] private GameObject _batteryVisual;

	private bool _launched;

	private void Start()
	{
		_batteryVisual.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_launched)
		{
			return;
		}

		IContactable contactable = other.GetComponent<IContactable>();

		if (contactable != null)
		{
			contactable.Contact(transform);
		}
	}

	public void Launch()
	{
		_launched = true;
		Launched?.Invoke();

		_batteryVisual.SetActive(true);
	}
}
