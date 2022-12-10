using UnityEngine;

public class FlashingLight
{
	private Light _light;

	private float _intensity;

	private float _flashingMaxTime;
	private float _flashingTime;

	private bool _isFlashing;

	public FlashingLight(Light light)
	{
		_light = light;
		_intensity = light.intensity;
	}

	public void Update()
	{
		if (_isFlashing)
		{
			if(_flashingTime > 0)
			{
				if(_flashingTime - Time.deltaTime > 0)
				{
					_flashingTime -= Time.deltaTime;
				}
				else
				{
					_flashingTime = 0;
				}
			}
			else
			{
				_isFlashing = false;
			}

			_light.intensity = Random.Range(0, _intensity);
		}
		else
		{
			_light.intensity = _intensity;
		}
	}

	public void Flash(float time)
	{
		_isFlashing = true;
		_flashingTime = _flashingMaxTime;

		_flashingMaxTime = time;
	}
}
