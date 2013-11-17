<?php
if($id_i>0){
	require_once('_conn.php');
	
	$query = 'SELECT * FROM items WHERE id_i='.$id_i;
	$result = mysql_query($query);
	$jsons = array();
	while($row = mysql_fetch_assoc($result)){global $jsons;
		$jsons[] = row2json($row);
	}
	if(count($jsons)==1)echo $jsons[0];
	else {
		//TODO multiple
		
	}
}else echo '$id_i needed';