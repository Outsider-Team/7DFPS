using UnityEngine;
using UnityEngine.Events;

public class GeneratorEventListener : MonoBehaviour
{
	[SerializeField] private Generator _generator;
	[SerializeField] private UnityEvent _events;

	private void OnGeneratorLaunched()
	{
		_events.Invoke();
	}

	private void OnEnable()
	{
		_generator.Launched += OnGeneratorLaunched;
	}

	private void OnDisable()
	{
		_generator.Launched -= OnGeneratorLaunched;
	}
}