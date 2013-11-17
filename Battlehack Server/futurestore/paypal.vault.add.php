<?php

require_once('_paypal.php');

$card = new CreditCard();
$card->setNumber('4417119669820331');
$card->setExpire_month('11');
$card->setExpire_year('2018');
$card->setFirst_name('Joe');
$card->setLast_name('Shopper');
$card->setType('visa');
$card->setPayer_Id('123456789');

$card->create($apiContext);