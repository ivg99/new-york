<?php

//require_once('/home/areality/public_html/futurestore/_debug.php');
if(!isset($ccid))echo 'ccid needed';
else if(!isset($price))echo 'price needed';
else{

	require_once('/home/areality/public_html/futurestore/paypal/rest-apis/requests.php');

	$paypal = new paypal();
	
$request = '{
  "intent":"sale",
  "redirect_urls":{
    "return_url":"http://www.return.com",
    "cancel_url":"http://www.cancel.com"
  },
  "payer":{
    "payment_method":"credit_card",
	"funding_instruments":[
	 {
		"credit_card_token":{
			"credit_card_id":"CARD-6FT46537T0847725XKKEJMLY"
		}
	 }
	]
  },
  "transactions":[
    {
      "amount":{
        "total":"'.$price.'",
        "currency":"USD"
      },
      "description":"This is the payment transaction description."
    }
  ]
}';

$response =  $paypal->process_payment($request) ;

$transactions = $response['body']->transactions;

for($i=0;$i<count($transactions);$i++){
	$resources = $transactions[$i]->related_resources;
	for($j=0;$j<count($resources);$j++){
		$state = $resources[$j]->sale->state;
		if($state=='completed'){
			$json['status'] = 'success';
			$json['id'] = $resources[$j]->sale->id;
			$jsonspew = json_encode($json);
			die($jsonspew);
		}
	}
}

}