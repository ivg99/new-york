using UnityEngine;
using System.Collections;

public abstract class ApplicationScreen : MonoBehaviour {

	[SerializeField] protected UITransformNode uiTransform;

	public virtual void Activate(bool immediate, int startPoint){
		activated = true;
	}
	public virtual void Deactivate(bool immediate, int endPoint){
		activated = false;
	}

	protected bool activated;
}
