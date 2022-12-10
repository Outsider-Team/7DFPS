using UnityEngine;

public abstract class GrabInteractive : Interactive
{
	protected override void OnInteract(Bathyscaph bathyscaph)
	{
		bathyscaph.Grab(this);
	}

	protected abstract void OnStopInteraction(Bathyscaph bathyscaph);

	public void StopInteraction(Bathyscaph bathyscaph)
	{
		OnStopInteraction(bathyscaph);
	}
}
