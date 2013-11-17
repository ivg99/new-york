<?php

if($submitted==1 && $id_i>0){
	$filepath = $_FILES['file']['tmp_name'];

	// TODO add azure storage

	if(strlen($_FILES['file']['name'])>2){
		$uniqid = uniqid('pic');
		$newpath = 'img/'.$uniqid.'.'.$_FILES['file']['name'];
		move_uploaded_file($filepath,$newpath);
			
		$urlpath = 'http://futurestore.areality3d.com/'.$newpath;
			
		require_once('_conn.php');
			
		$query = mupd('items','photo',$urlpath,'id_i',$id_i);
		mquery($query);
			
		require_once('image.resizer.php');
		$photo_large = ResizeImage(256,256,$urlpath);
		$photo_thumb = ResizeImage(64,64,$urlpath);

		$query = mupd('items','photo_thumb',$photo_thumb,'id_i',$id_i);
		mquery($query);	

		$query = mupd('items','photo_large',$photo_large,'id_i',$id_i);
		mquery($query);		
		
	}




}