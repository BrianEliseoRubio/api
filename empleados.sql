


CREATE TABLE Roles (
    IdRol INT PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
	Bonos DECIMAL(10,2) NOT NULL DEFAULT 0
);


INSERT INTO Roles (IdRol, Nombre, Bonos)
VALUES
    (1, 'Chofer',10.00),
    (2, 'Cargador',5.00),
    (3, 'Auxiliar',0.0);

CREATE TABLE Empleados (
    IdEmpleado INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Direccion NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
    Salario DECIMAL(10,2) NOT NULL,
	IdRol INT NOT NULL
	CONSTRAINT FK_Roles_Employees FOREIGN KEY (IdRol)
	REFERENCES Roles(IdRol)
);


-- Agregar un empleado
INSERT INTO Empleados (Nombre, Apellido, Direccion, Telefono, Salario,IdRol)
VALUES ('Juan', 'Pérez', 'Direcion', '1236457896', 30.00,1);

-- Agregar otro empleado
INSERT INTO Empleados (Nombre, Apellido, Direccion, Telefono, Salario,IdRol)
VALUES ('María', 'González', 'Direcion', '1236457896', 30.00, 1);

-- Agregar un tercer empleado
INSERT INTO Empleados (Nombre, Apellido, Direccion, Telefono, Salario,IdRol)
VALUES ('Pedro', 'Martínez', 'Direcion', '1236457896', 30.00, 2);

-- Agregar un cuarto empleado
INSERT INTO Empleados (Nombre, Apellido, Direccion, Telefono, Salario,IdRol)
VALUES ('Laura', 'López', 'Direcion', '1236457896', 30.00,3);

CREATE TABLE Entregas (
    IdEntrega INT PRIMARY KEY,
    IdEmpleado INT NOT NULL,
    FechaEntrega DATE NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Entregas_Employees FOREIGN KEY (IdEmpleado)
        REFERENCES Empleados(IdEmpleado)
);


CREATE PROCEDURE spObtenerEmpleados
AS
BEGIN
    SELECT *
    FROM Empleados
END


CREATE PROCEDURE spObtenerRoles
AS
BEGIN
    SELECT *
    FROM Roles
END



ALTER PROCEDURE spInsertarEmpleados(
	@Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @Direccion NVARCHAR(20),
    @Telefono NVARCHAR(20),
    @salario decimal,
	@idRol int
)
AS
BEGIN
    INSERT INTO Empleados(Nombre,Apellido,Direccion,Telefono,Salario,IdRol)
		VALUES(@nombre,@Apellido,@direccion,@telefono,@salario,@idRol)
END


CREATE PROCEDURE spActualizarEmpleados(
	@Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @Direccion NVARCHAR(20),
    @Telefono NVARCHAR(20),
    @salario decimal,
	@idRol int,
	@idEmpleado int
)
AS
BEGIN
	UPDATE Empleados
		SET Nombre = @nombre,
			Apellido = @Apellido,
			Direccion = @Direccion,
			Telefono = @Telefono,
			Salario = @salario,
			IdRol =   @idrol
	WHERE idEmpleado = @idEmpleado

END



ALTER PROCEDURE spGeneraNomina(
	@idEmpleado int,
	@entregas int,
	@total_sueldo_out decimal OUTPUT,
	@total_x_entregas_out decimal OUTPUT,
	@total_x_bonos_out decimal OUTPUT,
	@isr_out decimal OUTPUT,
	@vales_out decimal OUTPUT
)
AS
BEGIN
	DECLARE @sueldo decimal,
			@total_sueldo decimal,
			@precio_entrega decimal = 5.00,
			@bono int,
			@ISR decimal,
			@vales decimal,
			@total_sueldo_bono decimal;

	--obtenemos el salario del trabajaro en el cual son por 8 horas 6 dias a la semana tomando en cuenta 4 semanas
	SELECT @sueldo = e.Salario, @bono = r.Bonos FROM Empleados e
	LEFT JOIN Roles r ON r.IdRol = e.IdRol
	WHERE e.idEmpleado = @idEmpleado;

	--añadimos primero el salario base
	SET @total_sueldo = (@sueldo * 8) * 6;

	--añadimos el bono
	IF(@bono > 0)
	BEGIN
		SET @total_sueldo = @total_sueldo + (@bono *8) *6;
		SET @total_x_bonos_out = (@bono *8) *6;
	END
	
	
	---añadimos las entregas 
	SET @total_sueldo = @total_sueldo + (@entregas * @precio_entrega);
	SET @total_x_entregas_out =  (@entregas * @precio_entrega)

	-- Calculamos el ISR del 9%
	SET @ISR = @total_sueldo * 0.09;

	-- Si el total_sueldo es mayor a $10,000, se agrega un 3% adicional de ISR
	IF (@total_sueldo > 10000)
		SET @ISR = @ISR + (@total_sueldo * 0.03);

	--Ahora calculamos el sueldo mensual en vales de despensa
	SET @vales = @total_sueldo * 0.04;


	SET @vales_out = @vales
	SET @total_sueldo_out = @total_sueldo
	SET @isr_out = @ISR
END
