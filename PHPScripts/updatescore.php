<?php 
        $db = mysql_connect('localhost', 'iliyankordev', 'bggo2016') or die('Could not connect: ' . mysql_error()); 
        mysql_select_db('iliyankordev') or die('Could not select database');
 
        $name = mysql_real_escape_string($_GET['name'], $db); 
        $score = mysql_real_escape_string($_GET['score'], $db); 
        $hash = $_GET['hash'];
        
        $secretKey="BulgariaGo";

        $real_hash = md5($name . $score . $secretKey); 
        if($real_hash == $hash) { 
            $query = "UPDATE  `scores` SET  `score` = $score WHERE name =  '$name'"; 
            $result = mysql_query($query) or die('Query failed: ' . mysql_error()); 
        } 
?>