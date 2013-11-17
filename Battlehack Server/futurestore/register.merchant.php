<?php require_once('_session.php');

$id_u = $_SESSION['id_u'];

if($submitted==1){
	
	require_once('_conn.php');

	//$id_u = $_REQUEST['idu_mi'];
	$info = $_REQUEST['info'];
	$icon = $_REQUEST['icon'];
	
	$query = mins('merchantinfo',array('idu_mi','info','icon'),array($id_u,$info,$icon)); 
	mquery($query);
	
	$id_mi = mysql_insert_id();
	$json['id_mi'] = $id_mi;
	if($donotecho!=1)echo json_encode($json);
	else {
		
		$_SESSION['id_mi'] = $id_mi;
		header('Location: /dashboard');
	
	}
}else if($id_u>0){
	$title = 'Login';
	
	
	require_once('lay/lay.top.php');
	
	require_once('lay/lay.topbar.php');
	?>
    <div class="container">
                                    
              <form role="form" method="post"><input type="hidden" name="submitted" value="1" /><input type="hidden" name="donotecho" value="1" />
<div class="row">
<div class="col-md-4">              
  <div class="form-group">
    <label for="icon">Icon (reload for a different one)</label>
	  
      <input type="hidden" class="form-control" id="icon" name="icon"><br /> <img src="http://www.gravatar.com/avatar/100?s=256&d=identicon" />	
    </label>
  </div>              
</div>
<div class="col-md-8">    
   <h2>Create your <b>skillfully custom</b> Store!</h2>          
  <div class="form-group">
    <label for="name">Store Name</label>
    <input type="text" class="form-control" id="name" name="name" placeholder="Enter a name for your store">
  </div>
  <div class="form-group">
    <label for="info">Info</label>
    <textarea class="form-control" id="info" name="info" placeholder="Describe your store"></textarea>
  </div> 
  <button type="submit" class="btn btn-default">Submit</button>
</form>    
</div>	
<?php require_once('lay/lay.bot.php'); }else if($god_mode==1){
	?>
<form>
<div id="id_u"><label for="idu_mi">idu_mi</label> <input type="hidden" name="<?php echo $id_u; ?>" /></div>
<div id="name"><label for="name">name</label> <input type="text" name="name" /></div>
<div id="info"><label for="info">info</label> <input type="text" name="info" /></div>
<div id="icon"><label for="icon">icon</label> <input type="text" name="icon" /></div>
<input type="hidden" name="submitted" value="1" />
<input type="submit" />
</form>
<?php 	
}else echo 'id_u in session?';