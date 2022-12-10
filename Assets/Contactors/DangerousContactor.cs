using UnityEngine;

public class DangerousContactor : Contactor
{
	[SerializeField] private float _damageAmount;

	protected override void OnContact(Bathyscaph bathyscaph)
	{
		bathyscaph.ApplyDamage(gameObject, _damageAmount);
	}
}