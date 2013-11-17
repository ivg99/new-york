using UnityEngine;
using System.Collections;

public abstract class ApplicationScreen : MonoBehaviour {

	[SerializeField] UITransformNode uiTransform;

	public abstract void Activate();
	public abstract void Deactivate();
}
