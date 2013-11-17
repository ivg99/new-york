<?php
if($id_i>0){
	
	require_once('_conn.php');
	
	$query = 'SELECT * FROM parameters WHERE idi_p='.$id_i;
	$result2 = mysql_query($query);
	
	$params=array();
	while($row = mysql_fetch_assoc($result2)){
		$params[] = $row;
	}
	$json_params = json_encode($params);
	
	if($output==1)echo $json_params;
	
}else echo 'id_i needed';