<?php
if($submitted==1 && $id_i>0){
	$filepath = $_FILES['file']['tmp_name'];
	
		// TODO add azure storage
	
		if(strlen($_FILES['file']['name'])>2){
			$uniqid = uniqid('mesh');
			$newpath = 'objs/'.$uniqid.'.obj';
			move_uploaded_file($filepath,$newpath);
			
			$urlpath = 'http://futurestore.areality3d.com/objs/'.$uniqid.'.obj';
			
			require_once('_conn.php');
			
			$query = mupd('items','model',$urlpath,'id_i',$id_i);
			mquery($query);
			
			echo 'success';
			
		}
	
}else{ 	
?><html>
<body>

<form action="item.meshupload.php" method="post" enctype="multipart/form-data">
id_i: <input type="text" name="id_i" />
<input type="hidden" name="submitted" value="1" />
file: <label for="file">File</label><input type="file" name="file" id="file" /><br>
<input type="submit" name="submit" value="Submit">
</form>

</body>
</html>
<?php } ?>