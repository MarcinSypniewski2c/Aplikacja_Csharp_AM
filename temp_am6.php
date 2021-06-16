<?php


$sensor_data = [
	"temperature" => rand(0,10),
	"pressure" => rand(975,1050)
	];


echo json_encode($sensor_data);