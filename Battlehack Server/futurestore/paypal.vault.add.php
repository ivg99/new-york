<?php
//require_once('/home/areality/public_html/futurestore/_debug.php');
if(!isset($type))echo 'type needed';
else if(!isset($number))echo 'number';
else if(!isset($expire_month))echo 'expire_month';
else if(!isset($expire_year))echo 'expire_year';
else if(!isset($first_name))echo 'first_name';
else if(!isset($last_name))echo 'last_name';
else{

	require_once('/home/areality/public_html/futurestore/paypal/rest-apis/requests.php');
	
	$paypal = new paypal();
	
	$type = strtolower($type);
	$credit_card = array("type" => $type,
	                     "number" => $number,
	                     "expire_month" => $expire_month,
	                     "expire_year" => $expire_year,
	                     "first_name" => $first_name,
	                     "last_name" => $last_name);
	
	$response = $paypal->store_cc($credit_card);
	
	$bodyname = $response['body']->name; $cc_state = $response['body']->state;
	if($bodyname == 'VALIDATION_ERROR'){
		$details = $response['body']->details;
		for($i=0;$i<count($details);$i++){
			$issues['errors'][] = ReadableNames($details[$i]->field).': '.$details[$i]->issue;
		}
		$issues['error_count'] = count($details);
		echo json_encode($issues) ;
	}else if($cc_state=='ok'){
		$id_cc = $response['body']->id;
		$json['id_cc'] = $id_cc;
		$json['error_count'] = 0;
		echo json_encode($json);
	}

	
	
}

function ReadableNames($str){
	if($str == 'number')return 'Credit Card Number';
	else if($str=='expire_year')return 'Expiration Year';
	else if($str=='type')return 'Credit Card Type';
	else return $str;
}

?>
