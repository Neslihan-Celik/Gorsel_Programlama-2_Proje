CREATE database dbPersonel
use dbPersonel

CREATE TABLE tblpersonel
(
id INT IDENTITY(1,1) PRIMARY KEY,
ad VARCHAR(50) NOT NULL, 
soyad VARCHAR(50) NOT NULL,
cinsiyet CHAR(1) NOT NULL,
tel VARCHAR(11),
tc VARCHAR(11),
dtarihi DATE,
gorev VARCHAR(30),
gorevYeri VARCHAR(30),
foto VARCHAR(50),
istarih date,
egitim VARCHAR(25),
maas int,
yetki int,
kadi VARCHAR(25),
sifre VARCHAR(25),
);

INSERT INTO tblpersonel(ad,soyad,cinsiyet,tel,tc,dtarihi,gorev,gorevYeri,foto,istarih,egitim,maas,yetki,kadi,sifre)
VALUES
('nes','çelik','k','123456','123456','1996.03.25','müdür','istanbul','foto.png','2002.03.01','önlisans',1000,1,'nes','123456'),
('ahmet','demirayak','e','123456','123456','1998.03.25','pazarlama','istanbul','foto.png','2002.03.01','önlisans',1000,2,'asd','123456'),
('admin','admin','k','123456','123456','1996.03.25','admin','istanbul','foto.png','2002.03.01','lisans',1000,0,'admin','123456');

Alter Table tblpersonel 
Add mail VARCHAR(50)