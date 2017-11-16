<?php

header('Access-Control-Allow-Credentials: true');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');
header('Content-Type: application/json');
//game.php - main script, accepts all requests.

$inc = (int)$_REQUEST['i'];

$links = array();
$links[0] = 'act/example.php';
//$links[1] = 'act/example1.php';

if (isset($links[$inc])) {
    include $links[$inc];
} else {
    die(json_encode(array("err_msg" => "AUTH ERROR!")));
}

unset($inc, $links);

?>