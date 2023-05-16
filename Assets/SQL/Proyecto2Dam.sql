CREATE DATABASE IF NOT EXISTS ProyectoDamDb;
USE ProyectoDamDb;

-- Drop

DROP TABLE IF EXISTS Usuarios;
-- Create

CREATE TABLE IF NOT EXISTS Usuarios(
	IdUsuario INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
    Nick VARCHAR(31) UNIQUE NOT NULL,
    Password VARCHAR(64) NOT NULL, -- Cualquier contraseña Hasheada en SHA256 tiene 64 caracteres totales 
    Email VARCHAR(63) NOT NULL,
    Nombre VARCHAR(26) NOT NULL,
    Apellidos VARCHAR(60) NOT NULL,
    Puntuacion INT NOT NULL,
    FechaEntrada DATETIME NOT NULL,
    FechaUltimoInicioSesion DATETIME NULL,
    Dinero Decimal(11,2) NOT NULL,
    Estado ENUM('conectado','ausente','jugando','desconectado') not null,
    Intentos Decimal(1) NOT NULL,
    PartidasGanadas INT NOT NULL,
    PartidasPerdidas INT NOT NULL
);

-- Stored Procedures

DROP PROCEDURE IF EXISTS SpRegistroUsuario;
DROP PROCEDURE IF EXISTS SpLogin;
DELIMITER $$
CREATE PROCEDURE SpRegistroUsuario(	IN _nick VARCHAR(31),
									IN _password VARCHAR(64),
                                    IN _email VARCHAR(63),
									IN _nombre VARCHAR(26),
									IN _apellidos VARCHAr(60),
									OUT _resultado INT)
pa:
BEGIN
	DECLARE ExisteUsuario INT;
    -- Error -9999 | Error desconocido
    SET _resultado = -9999;
    
    -- Error -1 | nick esta vacio o es nulo
    IF _nick LIKE '' OR _nick LIKE null
    THEN
		SET _resultado = -1;
        LEAVE pa;
    END IF;
    
    -- Error -2 | Nombre esta vacio o es nulo
    IF _nombre LIKE '' OR _nombre LIKE null
    THEN
		SET _resultado = -2;
        LEAVE pa;
    END IF;
    
    -- Error -3 | Apellidos esta vacio o es nulo
    IF _apellidos LIKE '' OR _apellidos LIKE null
    THEN
		SET _resultado = -3;
        LEAVE pa;
    END IF;
    
    -- Error -4 | Contraseña esta vacia o es nula
    IF _password LIKE '' OR _password LIKE null
    THEN
		SET _resultado = -4;
        LEAVE pa;
    END IF;
    
    -- Error -5 | El usuario ya existe
    Select count(*)
    From Usuarios
    Where Nick Like _nick AND Password LIKE _passoword AND Email
    INTO ExisteUsuario;
    
    -- Error -6 | Correo Electronico no valido
    IF NOT _email REGEXP patron THEN
        SET _resultado = -6;
        LEAVE pa;
    END IF; 
    
    IF ExisteUsuario > 0 -- Si hay al menos un usuario
    THEN
		SET _resultado = -5;
        LEAVE pa;
    ELSE
		-- Error 0 | No dio errores
		INSERT INTO Usuarios
		VALUES (_nick,_Email,_password,_nombre,_apellidos,0,sysdate(),null,0,'desconectado',0,0,0);
        
		SET _resultado = 0;
    END IF;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS SpLogin;
DELIMITER $$
CREATE PROCEDURE SpLogin(	IN _nick VARCHAR(31),
							IN _password VARCHAR(64),
							OUT _resultado INT)
pa:
BEGIN
	DECLARE ExisteUsuario INT;
    DECLARE patron VARCHAR(127);
    DECLARE contador INT;
    Declare EstaBloqueado DECIMAL(1);
    
    SET contador = 0;
    SET patron = '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$';
    -- Error -9999 | Error desconocido
    SET _resultado = -9999;
    
    -- Error -1 | El nick está vacio
    IF _nick LIKE '' OR _nick IS NULL
    THEN
		SET _resultado = -1;
		
        Set contador = contador + 1;
        
        UPDATE Usuarios
        SET Intentos = contador
        Where email LIKE _email;
        
        LEAVE pa;
    END IF;
    
	-- Error -2 | La contraseña está vacia
    IF _password LIKE '' OR _password IS NULL
    THEN
		SET _resultado = -2;
        
        Set contador = contador + 1;
        
        UPDATE Usuarios
        SET Intentos = contador
        Where Nick LIKE _nick;
        
        LEAVE pa;
    END IF;
    
	-- Error -3 | El nick introducido no existe
    SELECT count(*)
    FROM Usuarios
    Where Nick LIKE _nick
    INTO ExisteUsuario;
    
    IF ExisteUsuario > 0
    THEN
		SET _resultado = -3;
        LEAVE pa;
    END IF;
    
    -- Error -4 | El usuario está bloqueado
    SELECT Intentos
    FROM Usuarios
    WHERE Nick = _nick
    INTO EstaBloqueado;
    
    If EstaBloqueado > 5 -- Sustituid este 5 por el numero de intentos máximos de inicio de sesión
    THEN
		SET _resultado = -4;
        LEAVE pa;
	END IF;
    
    
    
    -- Error 0 | No dio errores
    UPDATE Usuarios
    SET FechaUltimoInicioSesion = SYSDATE()
    WHERE Nick Like _nick;
    
    
    SET _resultado = 0;
    
    
END$$
DELIMITER ;




