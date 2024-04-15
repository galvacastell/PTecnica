
create database DBVentaXYZ
go

use DBVentaXYZ
go

create table Opcion(
codOpcion int primary key identity(1,1),
descripcion varchar(50),
padre int,
codControlador char(20),
codmetodo char(20),
rutapagina char(50),
marcaBorrado bit default 0,
fechaRegistro datetime default getdate()
)
go

create table Rol(
codRol int primary key identity(1,1),
descripcion varchar(50),
marcaBorrado bit default 0,
fechaRegistro datetime default getdate()
)
go

create table OpcionRol(
codOpcionRol int primary key identity(1,1),
codOpcion int references Opcion(codOpcion),
codRol int references Rol(codRol),
tipo int,   --0-Lectura/1-Registrar/2-Modificar/3-Eliminar
optEstado char(20) default '', --Estado del objeto puede actualizar el perfil 
marcaBorrado bit default 0,
fechaRegistro datetime default getdate()
)
go

create table Usuario(
codUsuario char(15) primary key,
nombre varchar(100),
correo varchar(40),
telefono varchar(12),
puesto varchar(40), 
codRol int references Rol(codRol),
clave varchar(40),
marcaBorrado bit default 0,
fechaRegistro datetime default getdate()
)
go


create table Producto (
codSku  char(15) primary key,
nombre varchar(100),
tipo varchar(20),
etiqueta varchar(100),
precio decimal,
und_medida varchar(10),
stock decimal,
fechaRegistro datetime default getdate(),
marcaBorrado bit default 0
)
go

create table Pedido(
codPedido int primary key identity(1,1),
fechaPedido datetime default getdate(),
fechaRecepcion datetime ,
fechaDespacho datetime ,
fechaEntrega datetime ,
codVendedor char(15) references Usuario(codUsuario) null,
codRepartidor char(15) null ,
estado int default 0,
marcaBorrado bit default 0
)
go

create table DetallePedido
(
 codDetalle int primary key identity(1,1),
 codPedido int references Pedido(codPedido),
 codSku char(15) references Producto(codSku),
 item int,
 cantidad decimal,
 fechaRegistro datetime default getdate(),
 marcaBorrado bit default 0
)
go

INSERT INTO Rol(descripcion) values('Administrador'); 
INSERT INTO Rol(descripcion) values('Encargado');
INSERT INTO Rol(descripcion) values('Vendedor');
INSERT INTO Rol(descripcion) values('Delivery');
INSERT INTO Rol(descripcion) values('Repartidor');
INSERT INTO Rol(descripcion) values('Cliente');

INSERT INTO Usuario(codUsuario,nombre,correo,telefono,puesto,codRol,clave) 
 values('Administrador','Administrador','','','Administrador',1,'123456');

INSERT INTO Usuario(codUsuario,nombre,correo,telefono,puesto,codRol,clave) 
 values('Encargado','Encargado','','','Encargado',2,'123456');

INSERT INTO Usuario(codUsuario,nombre,correo,telefono,puesto,codRol,clave) 
 values('Vendedor','Vendedor','','','Vendedor',3,'123456');

INSERT INTO Usuario(codUsuario,nombre,correo,telefono,puesto,codRol,clave) 
 values('Delivery','Delivery','','','Delivery',4,'123456');

 INSERT INTO Usuario(codUsuario,nombre,correo,telefono,puesto,codRol,clave) 
 values('Repartidor','Repartidor','','','Repartidor',5,'123456');

  INSERT INTO Usuario(codUsuario,nombre,correo,telefono,puesto,codRol,clave) 
 values('Cliente','Cliente','','','Cliente',6,'123456');

 INSERT INTO Opcion(descripcion,padre,codControlador) values('Pedido',0,'PEDIDO');
 INSERT INTO Opcion(descripcion,padre,codControlador) values('Usuarios',0,'USUARIO');
 INSERT INTO Opcion(descripcion,padre,codControlador) values('Productos',0,'PRODUCTO');

 INSERT INTO Opcion(descripcion,padre,codControlador) values('Mantenimiento Pedido',1,'PEDIDO');

 /*
 Estado orden
 Por atender  0
 En proceso   1
 En delivery  2
 Recibido     3
*/

 --Encargado
 insert into OpcionRol(codRol,codOpcion,tipo) values(2,1,0); --0-Pedido
 insert into OpcionRol(codRol,codOpcion,tipo) values(2,4,0); --0-Leer Pedido
 insert into OpcionRol(codRol,codOpcion,tipo) values(2,4,1); --1-Registrar Pedido
 insert into OpcionRol(codRol,codOpcion,tipo,optEstado) values(2,4,2,'0,1,2,3'); --2-Modificar Pedido
 insert into OpcionRol(codRol,codOpcion,tipo) values(2,4,3); --3-Eliminar Pedido

 --Vendedor
 insert into OpcionRol(codRol,codOpcion,tipo) values(3,1,0); --0-Pedido
 insert into OpcionRol(codRol,codOpcion,tipo) values(3,4,0); --0-Leer Pedido
 insert into OpcionRol(codRol,codOpcion,tipo,optEstado) values(3,4,2,'0'); --2-Modificar Pedido

 --Delivery
 insert into OpcionRol(codRol,codOpcion,tipo) values(4,1,0); --0-Pedido
 insert into OpcionRol(codRol,codOpcion,tipo) values(4,4,0); --0-Leer Pedido
 insert into OpcionRol(codRol,codOpcion,tipo,optEstado) values(4,4,2,'1,2'); --2-Modificar Pedido

 insert into OpcionRol(codRol,codOpcion,tipo) values(5,1,0); --0-Pedido
 insert into OpcionRol(codRol,codOpcion,tipo) values(5,4,0); --0-Leer Pedido
 insert into OpcionRol(codRol,codOpcion,tipo,optEstado) values(5,4,2,'1,2'); --2-Modificar Pedido
 insert into OpcionRol(codRol,codOpcion,tipo) values(6,4,1); --1-Registrar Pedido

  insert into Producto(codSku,nombre,tipo,etiqueta,precio,und_medida,stock)
  values('001','Manzana Israel',1,'FF:12152015, FC:01012026',15,'Unidad',150);
    insert into Producto(codSku,nombre,tipo,etiqueta,precio,und_medida,stock)
  values('002','Uba Gorgonia',1,'FF:12152015, FC:01012026',8,'Kilo',400);
    insert into Producto(codSku,nombre,tipo,etiqueta,precio,und_medida,stock)
  values('003','Arroz integral/bolsa',1,'FF:12152015, FC:01012026',3.5,'Unidad',200);
    insert into Producto(codSku,nombre,tipo,etiqueta,precio,und_medida,stock)
  values('004','Menestra Marron/bolsa',1,'FF:12152015, FC:01012026',6,'Unidad',350);

