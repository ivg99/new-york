<?php
if(isset($cc)){
	$first = substr($cc,0,1);
	$json=array();
	if($first==4)$json['issuer'] = 'VISA';
	else if($first==5)$json['issuer'] =  'MasterCard';
	else if($first==6)$json['issuer'] =  'Discover';
	else $json['issuer'] =  'AmEx';
	echo json_encode($json);
}else echo 'cc needed';