<?php

function param_group_form_sub($i){ global $param_name,$row; ?>
<div class="eachparam" param_id="<?php echo $i ?>">
<div id="transform" class="input-group">
<div class="form-group">
<label id="param_name_label" for="param_name">Param Name</label>
<input type="text" class="form-control" id="param_name" placeholder="Enter your parameter name (submesh name)" value="<?php echo $param_name ?>">
</div>
</div>
<div id="transform" class="input-group">
<h5>Transform</h5>
<div class="row">
<?php param_input_before('translate_x','x',$row['translate_x']);    ?>
<?php param_input_after('translate_x1','x',$row['translate_x1']);    ?>                    
                        </div>
                    	<div class="row">
<?php param_input_before('translate_y','y',$row['translate_y']);    ?>  
<?php param_input_after('translate_y1','y',$row['translate_y1']);    ?>                    
                        </div> 
                    	<div class="row">
<?php param_input_before('translate_z','z',$row['translate_z']);    ?>  
<?php param_input_after('translate_z1','z',$row['translate_z1']);    ?>                    
                        </div>                                               
                  </div>
                  <div id="transform" class="input-group">
                  	<h5>Rotate</h5>
                    	<div class="row">
<?php param_input_before('rotate_x','x',$row['rotate_x']);    ?>  
<?php param_input_after('rotate_x1','x',$row['rotate_x1']);    ?>                    
                        </div>
                    	<div class="row">
<?php param_input_before('rotate_y','y',$row['rotate_y']);    ?>  
<?php param_input_after('rotate_y1','y',$row['rotate_y1']);    ?>                    
                        </div> 
                    	<div class="row">
<?php param_input_before('rotate_z','z',$row['rotate_z']);    ?>  
<?php param_input_after('rotate_z1','z',$row['rotate_z1']);    ?>                   
                      </div>
                  </div> 
                  <div id="transform" class="input-group">
                  	<h5>Scale</h5>
                    	<div class="row">
<?php param_input_before('scale_x','x',$row['scale_x']);    ?>  
<?php param_input_after('scale_x1','x',$row['scale_x1']);    ?>                    
                        </div>
                    	<div class="row">
<?php param_input_before('scale_y','y',$row['scale_y']);    ?>  
<?php param_input_after('scale_y1','y',$row['scale_y1']);    ?>                    
                        </div> 
                    	<div class="row">
<?php param_input_before('scale_z','z',$row['scale_z']);    ?>  
<?php param_input_after('scale_z1','z',$row['scale_z1']);    ?>                   
                      </div>
                  </div>                                      
              </div> 
<?php }


if($echo==1&&$i>0) param_group_form_sub($i);




function param_input_before($id,$name,$val){
	?>					<div class="col-md-6">
                       	   <div class="input-group">
                               <span class="input-group-btn">
                                <button class="btn btn-default" type="button"><?php echo $name ?>min</button>
                              </span>                          
                              <input value="<?php echo $val ?>" id="<?php echo $id ?>" name="<?php echo $id ?>" type="text" class="form-control">

                            </div><!-- /input-group -->	
                        </div>
	<?php 
}
function param_input_after($id,$name,$val){
	?>
						<div class="col-md-6">
                       	   <div class="input-group">                         
                              <input value="<?php echo $val ?>" id="<?php echo $id ?>" name="<?php echo $id ?>" type="text" class="form-control">
                               <span class="input-group-btn">
                                <button class="btn btn-default" type="button"><?php echo $name ?>max</button>
                              </span> 
                            </div><!-- /input-group -->	
                        </div>
	<?php 
}