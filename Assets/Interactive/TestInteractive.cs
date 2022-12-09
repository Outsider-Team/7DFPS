using UnityEngine;

public class TestInteractive : MonoBehaviour, IInteractable
{
	public void Interact(object sender)
	{
		Destroy(gameObject);
	}
}
