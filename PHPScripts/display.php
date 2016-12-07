<?php
    $db = mysql_connect('localhost', 'iliyankordev', 'bggo2016') or die('Could not connect: ' . mysql_error()); 
    mysql_select_db('iliyankordev') or die('Could not select database');
 
    $query = "SELECT * FROM `scores` ORDER by `score` DESC LIMIT 20";
    $result = mysql_query($query) or die('Query failed: ' . mysql_error());
 
    $num_results = mysql_num_rows($result);  
 
    for($i = 0; $i < $num_results; $i++)
    {
         $row = mysql_fetch_array($result);
         echo $row['name'] . "\n" . $row['score'] . "\n";
    }
?>