<?php
if($id_i>0&&strlen($price)>0){
	require_once('_conn.php');
	$query = mupd('items','price',(float)$price,'id_i',$id_i);
	mquery($query);
}