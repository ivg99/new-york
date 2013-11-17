<?php


$u='areality_storefu';
$p='vxGF7sWcXmPP';
$db = 'areality_futurest';
		
$conn = mysql_connect('localhost',$u,$p);
$c0nn=$conn;

mysql_select_db($db);

require_once('libs/fn_mysql.php');

function row2json($checkthis,$row){
	$json = array();
	foreach($row as $key=>$val){
		$json[$key]=$val;
	}
	return stripslashes(json_encode($json)); 
}

/*
 * 	foreach($row as $key=>$val){
		if($key==$checkthis)
			if($val!='')
				$json[$key]=$val;
			else return '';
	}
 */