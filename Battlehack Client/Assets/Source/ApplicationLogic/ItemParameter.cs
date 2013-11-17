using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemParameter {
	public string name;

	public bool translate_x;
	public float translate_x_min;
	public float translate_x_max;

	public bool translate_y;
	public float translate_y_min;
	public float translate_y_max;

	public bool translate_z;
	public float translate_z_min;
	public float translate_z_max;

	public bool rotate_x;
	public float rotate_x_min;
	public float rotate_x_max;

	public bool rotate_y;
	public float rotate_y_min;
	public float rotate_y_max;

	public bool rotate_z;
	public float rotate_z_min;
	public float rotate_z_max;

	public bool scale_x;
	public float scale_x_min;
	public float scale_x_max;

	public bool scale_y;
	public float scale_y_min;
	public float scale_y_max;

	public bool scale_z;
	public float scale_z_min;
	public float scale_z_max;
}
