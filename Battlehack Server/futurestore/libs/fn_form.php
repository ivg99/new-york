<?php
function formfield($id,$type){
	return '<div id="'.$id.'"><label for="'.$id.'">'.$id.'</label> <input type="'.$type.'" name="'.$id.'" /></div>';
}