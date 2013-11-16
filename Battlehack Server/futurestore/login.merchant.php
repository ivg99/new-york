<?php
// outputs merchant id given user id 

if($submitted==1){
	require_once('_conn.php');
	
	$id = quoty($_REQUEST['id']);
	
	$query = "SELECT * FROM merchantinfo WHERE idu_mi=".$id;
	$result = mysql_query($query);
	
	while($row = mysql_fetch_assoc($result)){
		
		$id_mi = $row['id_mi'];
	
	}
	
	if($id_mi>0){
		$json['id_mi'] = $id_mi;
		echo json_encode($json);
	}else echo 'DNE';
	
}else{
?>	
<form>
id: <input text="" name="id" />
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 	
}