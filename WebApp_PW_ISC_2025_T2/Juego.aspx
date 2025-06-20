<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Juego Tipo Among Us</title>
    <style>
        body {
            margin: 0;
            overflow: hidden;
            background-color: black;
            color: white;
            text-align: center;
            font-family: Arial, sans-serif;
        }
        canvas {
            display: block;
            background: #222;
            margin: auto;
        }
        #timer {
            position: absolute;
            top: 10px;
            left: 50%;
            transform: translateX(-50%);
            font-size: 24px;
        }
    </style>
</head>
<body>
    <div id="timer">Tiempo: <span id="time">30</span>s</div>
    <canvas id="gameCanvas"></canvas>
    <script>
        const canvas = document.getElementById("gameCanvas");
        const ctx = canvas.getContext("2d");
        canvas.width = 800;
        canvas.height = 600;

        let player = { x: 400, y: 300, size: 20, color: "blue" };
        let enemies = [];
        let keys = {};
        let timeLeft = 30;
        let gameOver = false;

        function createEnemies() {
            for (let i = 0; i < 5; i++) {
                enemies.push({
                    x: Math.random() * canvas.width,
                    y: Math.random() * canvas.height,
                    size: 20,
                    color: "red",
                    speed: 1.5 + Math.random()
                });
            }
        }

        function drawCharacter(character) {
            ctx.fillStyle = character.color;
            ctx.fillRect(character.x, character.y, character.size, character.size);
        }

        function movePlayer() {
            if (keys["ArrowUp"]) player.y -= 3;
            if (keys["ArrowDown"]) player.y += 3;
            if (keys["ArrowLeft"]) player.x -= 3;
            if (keys["ArrowRight"]) player.x += 3;

            player.x = Math.max(0, Math.min(canvas.width - player.size, player.x));
            player.y = Math.max(0, Math.min(canvas.height - player.size, player.y));
        }

        function moveEnemies() {
            enemies.forEach(enemy => {
                let dx = player.x - enemy.x;
                let dy = player.y - enemy.y;
                let distance = Math.sqrt(dx * dx + dy * dy);
                enemy.x += (dx / distance) * enemy.speed;
                enemy.y += (dy / distance) * enemy.speed;
            });
        }

        function checkCollisions() {
            for (let enemy of enemies) {
                if (
                    player.x < enemy.x + enemy.size &&
                    player.x + player.size > enemy.x &&
                    player.y < enemy.y + enemy.size &&
                    player.y + player.size > enemy.y
                ) {
                    gameOver = true;
                    alert("¡Perdiste! Los enemigos te atraparon.");
                    location.reload();
                }
            }
        }

        function updateTimer() {
            if (!gameOver) {
                timeLeft--;
                document.getElementById("time").textContent = timeLeft;
                if (timeLeft <= 0) {
                    alert("¡Ganaste! Sobreviviste el tiempo.");
                    location.reload();
                }
            }
        }

        function gameLoop() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            movePlayer();
            moveEnemies();
            checkCollisions();
            drawCharacter(player);
            enemies.forEach(drawCharacter);
            if (!gameOver) requestAnimationFrame(gameLoop);
        }

        document.addEventListener("keydown", (event) => keys[event.key] = true);
        document.addEventListener("keyup", (event) => keys[event.key] = false);

        createEnemies();
        setInterval(updateTimer, 1000);
        gameLoop();
    </script>
</body>
</html>