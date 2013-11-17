<?php require_once('libs/fn_form.php'); 
if($submitted==1){
	
	require_once('_conn.php');
	$query = mins('parameters',array('idi_p','param_name','translate_x','translate_y','translate_z','translate_x1','translate_y1','translate_z1',
														  'rotate_x','rotate_y','rotate_z','rotate_x1','rotate_y1','rotate_z1',
														  'scale_x','scale_y','scale_z','scale_x1','scale_y1','scale_z1',
														  'color'),
					   array($id_i,$param_name,
					   									  $translate_x,$translate_y,$translate_z,$translate_x1,$translate_y1,$translate_z1,
					   									  $rotate_x,$rotate_y,$rotate_z,$rotate_x1,$rotate_y1,$rotate_z1,
					   									  $scale_x,$scale_y,$scale_z,$scale_x1,$scale_y1,$scale_z1,
					   									  $color));
	mquery($query);
	$id_p = mysql_insert_id();
	
	$json['id_p'] = $id_p;
	echo json_encode($json);
	
}else{

?>
<form>
<?php echo formfield('id_i', 'text');
echo formfield ('param_name','text');
echo formfield('translate_x','text');
echo formfield('translate_y','text');
echo formfield('translate_z','text');
echo formfield('translate_x1','text');
echo formfield('translate_y1','text');
echo formfield('translate_z1','text');
echo formfield('rotate_x','text');
echo formfield('rotate_y','text');
echo formfield('rotate_z','text');
echo formfield('rotate_x1','text');
echo formfield('rotate_y1','text');
echo formfield('rotate_z1','text');
echo formfield('scale_x','text');
echo formfield('scale_y','text');
echo formfield('scale_z','text');
echo formfield('scale_x1','text');
echo formfield('scale_y1','text');
echo formfield('scale_z1','text');
echo formfield('color','text');
?>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 
}