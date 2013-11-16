<?php


if($submitted==1){
	
	require_once('_conn.php');

	$u = $_REQUEST['username'];
	$p = $_REQUEST['password'];
	$e = $_REQUEST['email'];
	
	$query = mins('users',array('username','password','email'),array($u,$p,$e)); 
	mquery($query);
	
	$json['id_u'] = mysql_insert_id();
	echo json_encode($json);
	
}else{
	?>
<form>
<div id="username"><label for="username">username</label> <input type="text" name="username" /></div>
<div id="password"><label for="password">password</label> <input type="password" name="password" /></div>
<div id="email"><label for="email">email</label> <input type="text" name="email" /></div>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 	
}