<?php
if($submitted==1){

}else{ 	
?><html>
<body>

<form action="file.upload.php" method="post" enctype="multipart/form-data">
<input type="hidden" name="submitted" value="1" />
<label for="file">File</label><input type="file" name="file" id="file" /><br>
<input type="submit" name="submit" value="Submit">
</form>

</body>
</html>
<?php } ?>