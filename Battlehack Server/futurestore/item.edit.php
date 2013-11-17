<?php require_once('_session.php');

$title = ' edit item';
require_once('lay/lay.top.php');

require_once('lay/lay.topbar.php');

$id_i = $id;

if($id_i>0){
	require_once('_conn.php');
	
	$query = 'SELECT * FROM items JOIN parameters ON id_i = idi_p WHERE id_i='.$id_i ; //echo $query;
	$result = mysql_query($query);
	//$jsons = array();
	require_once('_genparams.php');$i=0;
	
	echo '<div class="container">';
	
	while($row = mysql_fetch_assoc($result)){
		if($i==0){
		echo '<div class="row"><div class="col-md-2"><h2>'.$row['name'].'</h2>';
		$photo_large = $row['photo_large'];
		echo '<img src="'.$photo_large.'" /></div><div class="col-md-2">&nbsp;</div><div class="col-md-6"><div class="form-group"><hr />
    <label for="info">Description</label>
    <textarea class="form-control" id="info" name="info" rows="10">'.$row['description'].'</textarea>
  </div> </div></div> <hr />';
		}
		
				$param_name = $row['param_name'];
				param_group_form_sub($i);
				$i++;
				echo '<hr />';
	}
	
	echo '<div>';
	
	
}else echo '$id_i needed';

require_once('lay/lay.bot.php');