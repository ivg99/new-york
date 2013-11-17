<?php
if($id_i>0){
	require_once('_conn.php');
	
	$query = 'SELECT * FROM items JOIN merchantinfo ON idmi_i = id_mi WHERE id_i='.$id_i ; //echo $query;
	$result = mysql_query($query);
	//$jsons = array();
	while($row = mysql_fetch_assoc($result)){
		$json = row2json($row);
	}
	$arrjson = json_decode($json);

	require_once('parameters.fetch.php');
	$arrjson->parameters = json_decode($json_params);	
	
	echo stripslashes(json_encode($arrjson));
	
}else echo '$id_i needed';