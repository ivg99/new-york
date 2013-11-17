<?php
if($submitted==1){
	
	require_once('_conn.php');

	// generate thumbnails
	require_once('image.resizer.php'); 
	$photo_large = ResizeImage(256,256,$photo);
	$photo_thumb = ResizeImage(64,64,$photo);
	
	$query = mins('items',array('name','photo','model','description','price','photo_large','photo_thumb'),array($name,$photo,$model,$description,$price,$photo_large,$photo_thumb));
	mquery($query);
	$id_i = mysql_insert_id();
	
	$json['id_i']='"'.$id_i.'"';
	
	echo stripslashes(json_encode($json));
	
}else{
?>
<form>
<div id="name"><label for="name">name</label> <input type="text" name="name" /></div>
<div id="photo"><label for="photo">photo</label> <input type="text" name="photo" /></div>
<div id="model"><label for="model">model</label> <input type="text" name="model" /></div>
<div id="description"><label for="description">description</label> <input type="text" name="description" /></div>
<div id="price"><label for="price">price</label> <input type="text" name="price" /></div>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 
}