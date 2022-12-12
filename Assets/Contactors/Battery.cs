using UnityEngine;

public class Battery : MonoBehaviour, IContactable
{
	public void Contact(Transform sender)
	{
		Generator generator = sender.GetComponent<Generator>();

		if(generator != null)
		{
			generator.Launch();
			Destroy(gameObject);
		}
	}
}
