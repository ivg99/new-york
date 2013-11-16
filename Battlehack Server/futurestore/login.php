<?php
// outputs user id given correct username password else DNE

require_once('_conn.php');

if($submitted==1){
	
	$u = quoty($_REQUEST['username']);
	$p = quoty($_REQUEST['password']);
	
	$query = sprintf("SELECT * FROM users WHERE username='%s' AND password='%s'",$u,$p);
	$result = mysql_query($query);
	
	while($row = mysql_fetch_assoc($result)){
		
		$id = $row['id_u'];
	
	}
	
	if($id>0){
		$json['id_u']=$id;
		echo json_encode($json);
	}else echo 'DNE';

}else{
	?>
<form>
<div id="username"><label for="username">username</label> <input type="text" name="username" /></div>
<div id="password"><label for="password">password</label> <input type="password" name="password" /></div>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 	
}