<?php require_once('_session.php');


if($submitted==1){
	
	require_once('_conn.php');

	$u = $_REQUEST['username'];
	$p = $_REQUEST['password'];
	$e = $_REQUEST['email'];
	
	$query = mins('users',array('username','password','email'),array($u,$p,$e));// echo $query;
	mquery($query);
	
	$id_u =  mysql_insert_id();
	$json['id_u'] = $id_u;
	if($fromform!=1)echo json_encode($json);
	else{
		$_SESSION['id_u'] = $id_u;
		$_SESSION['username'] = $u;
		header('Location: /dashboard');
	}
	
}else{
	$title = 'Register';
	
	
	require_once('lay/lay.top.php');
	
	require_once('lay/lay.topbar.php');	
	
	?>
    <div class="container">	
<form method="post" action="/register.php" role="form"><input type="hidden" name="form_submitted" value="1" /><input type="hidden" name="submitted" value="1" /><input type="hidden" name="fromform" value="1" />
  <div class="form-group">
    <label for="username">Username</label>
    <input type="text" class="form-control" id="username" name="username" placeholder="Username">
  </div>
  <div class="form-group">
    <label for="Password">Password</label>
    <input type="password" class="form-control" id="password" name="password" placeholder="Password">
  </div>
   <div class="form-group">
    <label for="Email">Email</label>
    <input type="text" class="form-control" id="email" name="email" placeholder="email">
  </div> 
  <div class="checkbox">
    <label>
      <input type="checkbox" name="merchantdash"> Take me straight to my Merchant Dashboard
    </label>
  </div>
  <button type="submit" class="btn btn-default">Register!</button>
</form>
</div>
<?php 	
require_once('lay/lay.bot.php');
}