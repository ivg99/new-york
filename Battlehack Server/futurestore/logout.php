<?php
require_once('_session.php');
unset($_SESSION);
session_destroy();
session_write_close();
header('Location: /');
die;