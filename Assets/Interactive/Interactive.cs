using UnityEngine;

public abstract class Interactive : MonoBehaviour, IInteractable
{
	protected abstract void OnInteract(Bathyscaph bathyscaph);

	public void Interact(object sender)
	{
		Bathyscaph bathyscaph = (Bathyscaph)sender;

		if (bathyscaph != null)
		{
			OnInteract(bathyscaph);
		}
	}
}