<?php
// Função para gerar uma cor aleatória em formato hexadecimal
function getRandomColor() {
    // Gera valores aleatórios para as componentes R, G e B da cor
    $red = dechex(mt_rand(0, 255));
    $green = dechex(mt_rand(0, 255));
    $blue = dechex(mt_rand(0, 255));
    
    // Garante que cada componente tenha dois dígitos
    $red = str_pad($red, 2, "0", STR_PAD_LEFT);
    $green = str_pad($green, 2, "0", STR_PAD_LEFT);
    $blue = str_pad($blue, 2, "0", STR_PAD_LEFT);
    
    // Retorna a cor no formato hexadecimal
    return "#$red$green$blue";
}

// Gera uma cor aleatória
$randomColor = getRandomColor();

// Define o tipo de conteúdo da resposta como JSON
header('Content-Type: application/json');

// Retorna a cor aleatória como um objeto JSON
echo json_encode(['color' => $randomColor]);
?>
