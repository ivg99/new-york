<?php
if(isset($cc)){
	$first = substr($cc,0,1);
	if($first==4)echo 'VISA';
	else if($first==5)echo 'MasterCard';
	else if($first==6)echo 'Discover';
	else echo 'AmEx';
}else echo 'cc needed';