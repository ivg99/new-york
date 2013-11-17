<?php

function param_group_form_sub($i){ ?>
<div class="eachparam" param_id="<?php echo $i ?>">
<div id="transform" class="input-group">
<div class="form-group">
<label id="param_name_label" for="param_name">Param Name</label>
<input type="text" class="form-control" id="param_name" placeholder="Enter your parameter name (submesh name)">
</div>
</div>
<div id="transform" class="input-group">
<h5>Transform</h5>
<div class="row">
<?php param_input_before('translate_x','x');    ?>
<?php param_input_after('translate_x1','x');    ?>                    
                        </div>
                    	<div class="row">
<?php param_input_before('translate_y','y');    ?>  
<?php param_input_after('translate_y1','y');    ?>                    
                        </div> 
                    	<div class="row">
<?php param_input_before('translate_z','z');    ?>  
<?php param_input_after('translate_z1','z');    ?>                    
                        </div>                                               
                  </div>
                  <div id="transform" class="input-group">
                  	<h5>Rotate</h5>
                    	<div class="row">
<?php param_input_before('rotate_x','x');    ?>  
<?php param_input_after('rotate_x1','x');    ?>                    
                        </div>
                    	<div class="row">
<?php param_input_before('rotate_y','y');    ?>  
<?php param_input_after('rotate_y1','y');    ?>                    
                        </div> 
                    	<div class="row">
<?php param_input_before('rotate_z','z');    ?>  
<?php param_input_after('rotate_z1','z');    ?>                   
                      </div>
                  </div> 
                  <div id="transform" class="input-group">
                  	<h5>Scale</h5>
                    	<div class="row">
<?php param_input_before('scale_x','x');    ?>  
<?php param_input_after('scale_x1','x');    ?>                    
                        </div>
                    	<div class="row">
<?php param_input_before('scale_y','y');    ?>  
<?php param_input_after('scale_y1','y');    ?>                    
                        </div> 
                    	<div class="row">
<?php param_input_before('scale_z','z');    ?>  
<?php param_input_after('scale_z1','z');    ?>                   
                      </div>
                  </div>                                      
              </div> 
<?php }


if($echo==1&&$i>0) param_group_form_sub($i);




function param_input_before($id,$name){
	?>					<div class="col-md-6">
                       	   <div class="input-group">
                               <span class="input-group-btn">
                                <button class="btn btn-default" type="button"><?php echo $name ?>min</button>
                              </span>                          
                              <input id="<?php echo $id ?>" name="<?php echo $id ?>" type="text" class="form-control">

                            </div><!-- /input-group -->	
                        </div>
	<?php 
}
function param_input_after($id,$name){
	?>
						<div class="col-md-6">
                       	   <div class="input-group">                         
                              <input id="<?php echo $id ?>" name="<?php echo $id ?>" type="text" class="form-control">
                               <span class="input-group-btn">
                                <button class="btn btn-default" type="button"><?php echo $name ?>max</button>
                              </span> 
                            </div><!-- /input-group -->	
                        </div>
	<?php 
}