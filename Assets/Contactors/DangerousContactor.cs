using UnityEngine;

public class DangerousContactor : Contactor
{
	[SerializeField] private float _damageAmount;

	protected override void OnContact(Bathyscaph bathyscaph)
	{
		base.OnContact(bathyscaph);

		bathyscaph.ApplyDamage(gameObject, _damageAmount);
	}
}