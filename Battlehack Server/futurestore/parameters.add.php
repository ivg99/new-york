<?php require_once('libs/fn_form.php'); 
if($submitted==1){
	
	require_once('_conn.php');
	$query = mins('parameters',array('idi_p','param_name','translate','rotate','scale','color'),array($id_i,$param_name,$translate,$rotate,$scale,$color));
	mquery($query);
	$id_p = mysql_insert_id();
	
	$json['id_p'] = $id_p;
	echo json_encode($json);
	
}else{

?>
<form>
<?php echo formfield('id_i', 'text');
echo formfield ('param_name','text');
echo formfield('translate','text');
echo formfield('rotate','text');
echo formfield('scale','text');
echo formfield('color','text');
?>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 
}