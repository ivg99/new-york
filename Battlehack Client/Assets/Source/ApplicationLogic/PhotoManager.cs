using UnityEngine;
using System.Collections;

public class PhotoManager : MonoBehaviour {
	private static PhotoManager instance;
	public static PhotoManager Instance{
		get{
			return instance;
		}
	}

	public const int LARGE_IMAGE_SIZE = 256;
	public const int THUMBNAIL_IMAGE_SIZE = 64;

	private ImageByID[] imageCache = new ImageByID[10];
	private int idx = 0;

	public Texture2D GetImage(int idx){
		for(int i=0; i<imageCache.Length; i++){
			if(imageCache[i].id == idx){
				if(imageCache[i].texture != null){
					return imageCache[i].texture;
				}
			}
		}
		return null;
	}


	void Awake(){
		instance = this;
		for(int i=0; i<imageCache.Length; i++){
			imageCache[i] = new ImageByID();
		}
	}


	public void LoadImage(int id, string url){

		if(GetImage(id) == null){

			ImageByID image = imageCache[idx];
			if(image.texture != null){
				Destroy(image.texture);
				image.texture = null;
			}
			image.id = id;
			image.url = url;
			
			StartCoroutine(LoadImage(image));


			idx = (idx +1)%imageCache.Length;
		}
	}


	IEnumerator LoadImage(ImageByID image){

		WWW www = new WWW( image.url );

		
        yield return www;
        if(www.error != null){
			Debug.LogError("ERROR ON PHOTO LOAD: "+ www.error);
		}
		else{

			//check to see if we've overwritten this load, before it happens
			if(www.url == image.url){
				Texture2D tex = www.texture;
				if(tex != null){
					image.texture = tex;
				}
				else{
					Debug.LogError("www.texture is null");
				}
			}
			else{
				Destroy(www.texture);
			}
		}

		yield return 0;
	}



}


[System.Serializable]
public class ImageByID{
	public int id;
	
	public string url;
	public Texture2D texture;
}
