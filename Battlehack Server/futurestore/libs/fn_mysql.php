<?php


// accepts number;

function quoty($txt){
	if(get_magic_quotes_gpc())$txt=stripslashes($txt);
	return mysql_real_escape_string($txt);
}

function mesc($txt,$conn){
	return mysql_escape_string($txt);
}

function mput($query,$conn){
	mysql_query($query,$conn) or $query = mysql_error($conn);

	return $query;
}

function mpoke($query,$conn){ // alias of mput
	return mput($query,$conn);
}

function mins($tab,$cubbyhole,$obj,$mult=0){
	// table, column name [array], objects of col [array]
	if($mult!=1){
		(!is_array($obj))?$obj=sprintf("'%s'",$obj):$que="array";
		if($que=='array'){
			$c="`".$cubbyhole[0]."`";
			if($obj[0]=='UUID()')
				$o='UUID()';
			else
				$o="'".$obj[0]."'";
	  for($i=1;$i<count($cubbyhole);$i++){
	  	$c.=",`".mysql_escape_string($cubbyhole[$i])."`";
	  	if($obj[$i]=='UUID()')
	  		$o.=',UUID()';
	  	else if(is_numeric($obj[$i]))
	  		$o.=','.$obj[$i];
	  	else
	  		$o.=",'".mysql_escape_string($obj[$i])."'";
	  }
	  $cubbyhole=$c;
	  $obj=$o;
		}

		$que=sprintf("INSERT INTO `%s` (%s) VALUES (%s)",$tab,$cubbyhole,$obj);
	}else{
		(!is_array($obj))?$obj=sprintf("'%s'",$obj):$que="array";
		if($que=='array'){
			$c=$cubbyhole[0];

			for($i=1;$i<count($cubbyhole);$i++){
				$c.=",".mysql_escape_string($cubbyhole[$i]);
	  }

	  for($j=0;$j<count($obj);$j++){
	  	$o.="('".$obj[$j][0]."'";
		  for($i=1;$i<count($cubbyhole);$i++){
		  	if($obj[$j][$i]=='UUID()')
		  		$o.=',UUID()';
		  	else
		  		$o.=",'".mysql_escape_string($obj[$j][$i])."'";
		  }
		  $o.="),";////
	  }
	  $cubbyhole=$c;
	  $obj=substr($o,0,strlen($o)-1);
		}

		$que=sprintf("INSERT INTO %s (%s) VALUES %s",$tab,$cubbyhole,$obj);
	}
	return $que;
}


function mupd($tab,$cubbyhole,$obj,$key,$keyval,$orderby='',$limit=' LIMIT 1'){
	// table, col name [array], obj of col [array], row key to fetch, keyvalue
	(!is_array($cubbyhole))?$que=sprintf("UPDATE %s SET %s='%s' WHERE %s='%s'",$tab,$cubbyhole,$obj,$key,$keyval):$que="array";
	if($que=='array'){
		$que=sprintf("UPDATE %s SET %s='%s'",$tab,mysql_escape_string($cubbyhole[0]),mysql_escape_string($obj[0]));
		for($i=1;$i<count($cubbyhole);$i++){
			$que.=sprintf(", %s='%s'",$cubbyhole[$i],$obj[$i]);
		}
		if(is_numeric($keyval))
			$que.=' WHERE '.$key.'='.$keyval;
		else
			$que.=sprintf(" WHERE %s='%s'",$key,$keyval);
	}
	if($orderby!='')$que.=' ORDER BY '.$orderby;
	if($limit!='')$que.=' '.$limit;
	return ltrim($que);
}

function mget($query,$conn){
	// $query = mysql_real_escape_string($query,$conn);
	$a=mysql_query($query,$conn) or $query = mysql_error($conn);

	if($a!=""){
		while($row=@mysql_fetch_array($a)) {
			$ret[] = $row;
		}
		@mysql_free_result($a);
	}else $ret=$query;

	return $ret;
}


function mss($txt){
	return stripslashes($txt);
}

function mquery($query){global $c0nn;
mysql_query($query,$c0nn) or die ( mail('yosofun@gmail.com','OSLborked @ called by '.$_SERVER['SCRIPT_NAME'], ' occurs at '.$_SERVER['HTTP_HOST'].$_SERVER['REQUEST_URI'].' from '.$_SERVER['REMOTE_ADDR'].' at '.gethostbyaddr($_SERVER['REMOTE_ADDR']).' ('.$_SERVER['HTTP_USER_AGENT'].")\n\n".serialize(debug_backtrace())."\n\n".mysql_error($c0nn)) );
}
/*
 function mailerror(){
mail('yosofun@gmail.com','ARborked @ called by '.$_SERVER['SCRIPT_NAME'],'occurs at '.$_SERVER['HTTP_HOST'].$_SERVER['REQUEST_URI'].' from '.$_SERVER['REMOTE_ADDR'].' at '.gethostbyaddr($_SERVER['REMOTE_ADDR']).' ('.$_SERVER['HTTP_USER_AGENT'].")\n\n".serialize(debug_backtrace())."\n\n".mysql_error($c0nn));
}*/

$datetime=date('Y-m-d H:i:s');
$timestamp=time();

?>