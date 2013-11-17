<?php
// see https://github.com/yosun/php-image-resize - returns $newurl (unless $notincluded, then echos)
require_once('libs/ImageResize.php');

function ResizeImage($x,$y,$imageurl){
	if($x>0 && $y>0 && strlen($imageurl)>3){
	
		$data = parse_url(str_replace('/','!',$imageurl));
		$newfilename = $data['scheme'].'~'.$data['host'].'~'.$x.'~'.$y.$data['path'];
		$newfilepath = 'temp/'.$newfilename;
		$newsavedfilepath = 'resized/'.$newfilename;
		
		$baseurl = 'http://futurestore.areality3d.com';
		
		if(!file_exists($newsavedfilepath)){
			$handle = copy($imageurl,$newfilepath);
			$image = new ImageResize($newfilepath);
			$image->crop($x,$y);
			$image->save($newsavedfilepath);
			$newurl = $baseurl.'/'.$newsavedfilepath;
		}else $newurl= $baseurl.'/'.$newsavedfilepath;
		return $newurl;
	}else return 'need x,y,imageurl';
}

if($notincluded==1)
	echo ResizeImage($x,$y,$imageurl);