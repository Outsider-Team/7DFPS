using UnityEngine;

public abstract class Contactor : MonoBehaviour, IContactable
{
	protected abstract void OnContact(Bathyscaph bathyscaph);

	public void Contact(object sender)
	{
		Bathyscaph bathyscaph = (Bathyscaph)sender;

		if(bathyscaph != null)
		{
			OnContact(bathyscaph);
		}
	}
}
