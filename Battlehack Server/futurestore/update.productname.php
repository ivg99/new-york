<?php
if($id_i>0&&strlen($name)>1){
	require_once('_conn.php');
	$query = mupd('items','name',$name,'id_i',$id_i);
	mquery($query);
}