using UnityEngine;

public class BathyscaphProjector : MonoBehaviour
{
	[SerializeField] private Light _mainLight;
	[SerializeField] private Bathyscaph _bathyscaph;

	private FlashingLight _flashingLight;

	private void OnEnable()
	{
		_bathyscaph.Damaged += BathyscaphOnDamaged;
	}

	private void OnDisable()
	{
		_bathyscaph.Damaged -= BathyscaphOnDamaged;
	}

	private void Start()
	{
		_flashingLight = new FlashingLight(_mainLight);
	}

	private void Update()
	{
		_flashingLight.Update();
	}

	private void BathyscaphOnDamaged(GameObject sender, float amount)
	{
		_flashingLight.Flash(1);
	}
}