using UnityEngine;

public abstract class Contactor : MonoBehaviour, IContactable
{
	protected abstract void OnContact(Bathyscaph bathyscaph);

	public void Contact(Transform sender)
	{
		Bathyscaph bathyscaph = sender.GetComponent<Bathyscaph>();

		if(bathyscaph != null)
		{
			OnContact(bathyscaph);
		}
	}
}
