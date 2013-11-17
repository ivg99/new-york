using UnityEngine;
using System.Collections;


[SerializeField]
public class ItemEntityLoader {


	private ItemEntity entity;
	
	public ItemEntity Entity{
		get{
			return entity;
		}
		set{
			entity = value;
		}
	}

	public bool Loaded{
		get{
			return (entity != null);
		}
	}
}
