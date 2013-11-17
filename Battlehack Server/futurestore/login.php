<?php require_once('_session.php');
// outputs user id given correct username password else DNE

require_once('_conn.php');
$msg='Welcome!';
if($submitted==1){
	
	$u = quoty($_REQUEST['username']);
	$p = quoty($_REQUEST['password']);
	
	$query = sprintf("SELECT * FROM users WHERE username='%s' AND password='%s'",$u,$p);
	$result = mysql_query($query);
	
	while($row = mysql_fetch_assoc($result)){
		
		$id = $row['id_u'];
	
	}
	
	if($id>0){
		if($form_submitted==1){
			$_SESSION['id_u'] = $id;
			$_SESSION['username'] = $row['username'];
			header('Location: /dashboard');
		}else{
			$json['id_u']=$id;
			echo json_encode($json);
		}
	}else{ 
		if($form_submitted==1){
			$msg = 'Incorrect username or password';
		}else{
			echo 'DNE';
		}
	}

	
	$submitted=0;
}else if($god_mode==1){
	?>
<form method="post" action="/login.php">
<input type="hidden" name="submitted" value="1" /><input type="hidden" name="form_submitted" value="1" />
<div id="username"><label for="username">username</label> <input type="text" name="username" /></div>
<div id="password"><label for="password">password</label> <input type="password" name="password" /></div>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 	
} if($submitted!=1 && $god_mode!=1 && $unity!=1){

$title = 'Login';


require_once('lay/lay.top.php');

require_once('lay/lay.topbar.php');

?>
    <div class="container">
    <div class="alert alert-warning">
    <?php echo $msg; ?>
    </div>
<form method="post" action="/login.php" role="form"><input type="hidden" name="form_submitted" value="1" /><input type="hidden" name="submitted" value="1" />
  <div class="form-group">
    <label for="username">Username</label>
    <input type="text" class="form-control" id="username" name="username" placeholder="Username">
  </div>
  <div class="form-group">
    <label for="Password">Password</label>
    <input type="password" class="form-control" id="password" name="password" placeholder="Password">
  </div>
  <div class="checkbox">
    <label>
      <input type="checkbox" name="merchantdash"> Take me straight to my Merchant Dashboard
    </label>
  </div>
  <button type="submit" class="btn btn-default">Login!</button>
</form>
</div>
<?php require_once('lay/lay.bot.php'); }

