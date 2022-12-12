using UnityEngine;

public class Battery : MonoBehaviour, IContactable
{
	public void Contact(object sender)
	{
		Generator generator = (Generator)sender;

		if(generator != null)
		{
			generator.Launch();
			Destroy(gameObject);
		}
	}
}
