<?php require_once('_session.php');

$included=1;$submitted=1; $id = $_SESSION['id_u'];

if($id>0){
require_once('login.merchant.php');



$query = 'SELECT * FROM items WHERE idmi_i='.$id_mi;
$result = mysql_query($query);


$title = 'Dashboard';


require_once('lay/lay.top.php');

require_once('lay/lay.topbar.php'); echo '<div class="container">';

echo '<div class="panel panel-default"><table class="table">'; $count=0;
while($row = mysql_fetch_array($result)){
	if( $row['name']!="") { ?>
<tr>
<td>
<a href="#"><img src="<?php echo $row['photo_thumb'] ?>" /></a>
</td>
<td>
<a href="#"><h2><?php echo $row['name'] ?></h2></a><?php echo $row['description'] ?>
</td>
</tr>  
	<?php $count++;
	}
}
echo '</table></div>';

if($count<1)echo'    <div class="alert alert-warning">You have no products. <a href="/create">Add a product!</a></div>';
echo '</div>';
require_once('lay/lay.bot.php');

}else echo 'You are not logged in';
