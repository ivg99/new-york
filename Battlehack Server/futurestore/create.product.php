<?php require_once('_session.php');
$id_mi = $_SESSION['id_mi'];
//TODO test uploads
if($id_mi>0){
	
	require_once('_conn.php');
	$query = mins('items',array('idmi_i'),array($id_mi));
	mquery($query);
	$id_i = mysql_insert_id();

$title = 'Create - Product';

$headerstuff='<link href="http://cdnjs.cloudflare.com/ajax/libs/dropzone/3.7.1/css/dropzone.min.css" rel="stylesheet">
    <script src="http://cdnjs.cloudflare.com/ajax/libs/dropzone/3.7.1/dropzone.js"></script>';

require_once('lay/lay.top.php');

require_once('_genparams.php');

$bot_script='		$("#parameters").hide();
var totalparams=1;
		$("#name").focusout(function(data){
			$.post("/update.productname.php",{id_i:'.$id_i.',name: $(this).val()},function(data){
				$("#name_label").html("Product Name:");
			});
		}).focus(function(){
				$("#name_label").html("<i>* Product Name:</i>");
		});	
					
		$("#price").focusout(function(data){
			$.post("/update.price.php",{id_i:'.$id_i.',price: $(this).val()},function(data){
				$("#price_label").html("Price:");
			});
		}).focus(function(){
				$("#price_label").html("<i>* Price:</i>");
		});		

		$("#description").focusout(function(data){
			$.post("/update.description.php",{id_i:'.$id_i.',description: $(this).val()},function(data){
				$("#description_label").html("Description:");
			});
		}).focus(function(){
				$("#description_label").html("<i>* Description:</i>");
		});		

		$("#AddAnotherParam").click(function(){
					totalparams++;  
			$.post("/_genparams.php?echo=1&i="+totalparams,function(data){
					
					$("#parameters-body").append("<hr />"+data);
			});
		});			
		
		Dropzone.options.myAwesomeDropzone = {
		  paramName: "file", // The name that will be used to transfer the file
		  maxFilesize: 500, // MB
		  acceptedFiles:"image/*,application/obj,.obj",
		  init: function(){this.on("complete", function(file) {
			  
			  
			});},
		  accept: function(file, done) {
			var ext = getFileExtension(file.name);
			if(ext!="invalid"){
				if(ext == "obj"){
					this.options.url="/item.meshupload.php?id_i='.$id_i.'";
					Dropzone.options.myAwesomeDropzone.url=this.options.url;	
					$("#parameters").show();				
				}else {
					this.options.url="/item.imageupload.php?id_i='.$id_i.'";
					Dropzone.options.myAwesomeDropzone.url=this.options.url;					
				}
				done();
			}else {
				done("Only .obj and image files (.png, .jpg, .gif) accepted.");
			}
		  }
		};	
							
	   $("#CreateMyProduct").click(function(){//alert("!");
				/*			alert(
			$(".eachparam").attr("param_id");			);*/
				$("#formbody").html("<center><h1>SUCCESS!</h1></center>");
	   });

function getFileExtension(filename){
	var a = filename.split(".");
	if( a.length === 1 || ( a[0] === "" && a.length === 2 ) )
		return "invalid";
	var ext = a.pop().toLowerCase();
	if(ext=="jpg" || ext=="gif" || ext=="png" || ext=="obj")
		return ext;
	return "invalid";
}';

require_once('lay/lay.topbar.php');

?>
    <div class="container" id="formbody">
        <form role="form">
        	<input type="hidden" name="id_i" value="<?php echo $id_i; ?>" />
        	<div class="row">
           	<div class="col-md-6">
                  <div class="form-group">
                    <label id="name_label" for="name">Product Name</label>
                    <input type="text" class="form-control" id="name" placeholder="Enter your product name">
                  </div>
               </div>
               <div class="col-md-6">   
                  <div class="form-group">
                    <label for="price" id="price_label">Price</label>
                    <input type="text" class="form-control" id="price" placeholder="How much is it?">
                  </div>  
               </div>
          </div>          
          <div class="form-group">
            <label for="description" id="description_label">Description</label>
            <textarea class="form-control" id="description" placeholder="Describe your product"></textarea>
          </div>                    
        </form>
		
              <label>Drop in this box everything you want to share for this project (product images and mesh file):</label>
              <form action="replaceme" class="dropzone" id="myAwesomeDropzone">
              <input type="hidden" name="submitted" value="1" />
              <input type="hidden" name="id_i" id="id_i" value="<?php echo $id_i; ?>" />
               Be sure to include: <ul>
               						<li>a sample image (used in your product listings)</li>
                                    <li>(optional) 3d mesh file in .obj format</li>
                                   </ul>
               <div class="form-group">
                    <!--div class="fallback">
                        <label for="file">Drop everything you want to share for this project (image file named icon, .obj, and screenshot images):</label>	
                        <input type="file" name="file[]" class="hideme" />    
                    </div--> 
                </div>
              </form>  
              <hr />
              <div id="parameters" class="input-group"> 
              <div id="parameters-body" class="input-group"> 
<?php param_group_form_sub(1); ?>
</div>
<hr />
<button class="btn btn-default" id="AddAnotherParam">Add Another Parameter...</button>   
              </div>
              <hr />
<button type="submit" class="btn btn-default" id="CreateMyProduct">Create My Product!</button>              
<?php 

require_once('lay/lay.bot.php');

}else die('id_mi');

