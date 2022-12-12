using UnityEngine;
using TMPro;

public class BathyscaphUI : MonoBehaviour
{
	[SerializeField] private Bathyscaph _bathyscaph;

	[SerializeField] private TextMeshProUGUI _safetyText;

	private void OnEnable()
	{
		_bathyscaph.SafetyChanged += OnBathyscaphSafetyChanged;
	}

	private void OnBathyscaphSafetyChanged(float value)
	{
		_safetyText.text = $"safety {value}";
		_safetyText.material.SetFloat("Face Dilate", 1f);
	}

	private void OnDisable()
	{
		_bathyscaph.SafetyChanged -= OnBathyscaphSafetyChanged;
	}
}
