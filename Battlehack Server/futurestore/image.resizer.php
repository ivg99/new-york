<?php
// see https://github.com/yosun/php-image-resize
require_once('libs/ImageResize.php');

if($x>0 && $y>0 && isset($imageurl)){

	$data = parse_url(str_replace('/','!',$imageurl));
	$newfilename = $data['scheme'].'~'.$data['host'].'~'.$x.'~'.$y.$data['path'];
	$newfilepath = 'temp/'.$newfilename;
	$newsavedfilepath = 'resized/'.$newfilename;
	
	$baseurl = 'http://futurestore.areality3d.com';
	
	if(!file_exists($newfilepath)){
		$handle = copy($imageurl,$newfilepath);
		$image = new ImageResize($newfilepath);
		$image->crop($x,$y);
		$image->save($newsavedfilepath);
		echo $baseurl.'/'.$newsavedfilepath;
	}else echo $baseurl.'/'.$newsavedfilepath;

}else echo 'need x,y,imageurl';