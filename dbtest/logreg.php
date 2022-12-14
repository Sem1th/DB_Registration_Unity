<?php
include "db.php";


$dt = $_POST;

$playerData = array(
	"id" => 0,
	"nickname" => "Name",
	"email" => "Email",
);

$error = array(

	"errorText" => "empty",
	"isError" => false,
);

$userData = array(
	"playerData" => $playerData,
	"error" => $error
);

if ($dt['type'] == "logging"){
	if (isset($dt['login']) && isset($dt['password'])){
		$users = $db->query("SELECT * FROM `users` WHERE `login` = '{$dt['login']}'");

		if ($users->rowCount() == 1){
            $user = $users->fetch(PDO::FETCH_ASSOC);

            if (password_verify($dt['password'], $user['password'])){

                $data = $db->query("SELECT * FROM `data` WHERE `userid` = {$user['id']}")->fetch(PDO::FETCH_ASSOC);


                $userData["playerData"]["id"]          = $user['id'];
				$userData["playerData"]["nickname"]    = $data['nickname'];
				$userData["playerData"]["email"]   	   = $data['email'];
            }else{
                SetError("Password or User uncorrenct");
            }
        }else{
            SetError("Password or User uncorrenct");
        }
    }
	
}else 
	if ($dt['type'] == "register"){
	if (isset($dt['login']) && isset($dt['password1']) && isset($dt['password2']) && isset($dt['nickname']) && isset($dt['email'])){
		
		$users = $db->query("SELECT * FROM `users` WHERE `login` = '{$dt['login']}'");
		if ($users->rowCount() == 0){
			if ($dt['password1'] == $dt['password2']){
				
				$hash = password_hash($dt['password1'], PASSWORD_DEFAULT);
				//Создание пользователя
				$db->query("INSERT INTO `users`(`login`, `password`) VALUES ('{$dt['login']}','{$hash}')");
				//Создание данных об игроке
                $db->query("INSERT INTO `data`(`userID`, `email`, `nickname`) VALUES ({$db->lastInsertId()},'{$dt['email']}','{$dt['nickname']}')");
				
				}else{
				SetError("Password");
				}
		}else{
			SetError("User Exists");
		}	
	}
}else
if ($dt['type'] == "save"){
    if (isset($dt['id']) && isset($dt['email']) && isset($dt['nickname'])){
        $db->query("UPDATE `data` SET `nickname`='{$dt['nickname']}', `email`='{$dt['email']}' WHERE `userid` = {$dt['id']}");
    }else{
        SetError("Save Data");
    }
}else{
    SetError("Unknown data");
}



function SetError($text){
	global $userData;
	$userData["playerData"] = null;
	$userData["error"]["isError"] = true;
	$userData["error"]["errorText"] = "Error: ".$text;
}

echo json_encode($userData, JSON_UNESCAPED_UNICODE);

?>


