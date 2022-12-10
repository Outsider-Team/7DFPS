using UnityEngine;

public class Contactor : MonoBehaviour, IContactable
{
	protected virtual void OnContact(Bathyscaph bathyscaph)
	{
		Debug.Log("Contact");
	}

	public void Contact(object sender)
	{
		Bathyscaph bathyscaph = (Bathyscaph)sender;

		if(bathyscaph != null)
		{
			OnContact(bathyscaph);
		}
	}
}
