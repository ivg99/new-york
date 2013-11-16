<?php


if($submitted==1){
	
	require_once('_conn.php');

	$id_u = $_REQUEST['idu_mi'];
	$info = $_REQUEST['info'];
	$icon = $_REQUEST['icon'];
	
	$query = mins('merchantinfo',array('id_u','info','icon'),array($id_u,$info,$icon)); 
	mquery($query);
	
	$json['id_mi'] = mysql_insert_id();
	echo json_encode($json);
	
}else{
	?>
<form>
<div id="id_u"><label for="idu_mi">idu_mi</label> <input type="text" name="idu_mi" /></div>
<div id="name"><label for="name">name</label> <input type="text" name="name" /></div>
<div id="info"><label for="info">info</label> <input type="text" name="info" /></div>
<div id="icon"><label for="icon">icon</label> <input type="text" name="icon" /></div>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 	
}