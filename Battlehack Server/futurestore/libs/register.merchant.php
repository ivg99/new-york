<?php


if($submitted==1){

	$query = mins('merchantinfo',array('idu_mi','storename','info','icon'),array($id_u,$storename,$info,$icon));
	mquery($query);
	$id_mi = mysql_insert_id();
	
	$json['id_mi'] = $id_mi;
	
	echo json_encode($json);

}else{
	?>
<form>
<div id="id_u"><label for="id_u">id_u</label> <input type="text" name="id_u" /></div>
<div id="info"><label for="info">info</label> <input type="text" name="info" /></div>
<div id="icon"><label for="icon">icon</label> <input type="text" name="icon" /></div>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 	
}