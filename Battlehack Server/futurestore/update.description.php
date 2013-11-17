<?php
if($id_i>0&&strlen($description)>1){
	require_once('_conn.php');
	$query = mupd('items','description',$description,'id_i',$id_i);
	mquery($query);
}