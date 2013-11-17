<?php

//$ccid = "CARD-35Y54265JC7133454KFM5G4I";

//require_once('/home/areality/public_html/futurestore/_debug.php');
if(!isset($ccid))echo 'ccid needed';
else{

	require_once('/home/areality/public_html/futurestore/paypal/rest-apis/requests.php');

	$paypal = new paypal();

	$response = $paypal->fetch_cc($ccid);
	
	$state = $response['body']->state;
	
	if($state == 'ok'){
		$json['state'] = 'ok';
	}else {
		$json['state'] = 'expired';
	}
	
	echo json_encode($json);
	
}