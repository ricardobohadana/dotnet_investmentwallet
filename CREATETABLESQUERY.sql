﻿CREATE TABLE PERFILINVESTIDOR (
	IDPERFILINVESTIDOR			UNIQUEIDENTIFIER		NOT NULL,
	TIPO					NVARCHAR(50)			NOT NULL,
	DESCRICAO				NVARCHAR(300)			NOT NULL,
	PRIMARY KEY(ID)
)

CREATE TABLE USUARIO (
	IDUSUARIO				UNIQUEIDENTIFIER		NOT NULL,
	IDPERFILINVESTIDOR			UNIQUEIDENTIFIER		NOT NULL,
	NOME					NVARCHAR(100)			NOT NULL,
	SENHA					NVARCHAR(50)			NOT NULL,
	EMAIL					NVARCHAR(50)			NOT NULL UNIQUE,
	PRIMARY KEY(ID),
	FOREIGN KEY(IDPERFILINVESTIDOR) REFERENCES PERFILINVESTIDOR(ID)
)

CREATE TABLE TIPOOPERACAO (
	IDTIPOOPERACAO				UNIQUEIDENTIFIER		NOT NULL,
	NOME					UNIQUEIDENTIFIER		NOT NULL UNIQUE,
	PRIMARY KEY(IDTIPOOPERACAO)
)

CREATE TABLE TIPOATIVO (
	IDTIPOATIVO				UNIQUEIDENTIFIER		NOT NULL,
	NOME					UNIQUEIDENTIFIER		NOT NULL UNIQUE,
	PRIMARY KEY(IDTIPOATIVO)
)

CREATE TABLE CARTEIRA (
	IDCARTEIRA				UNIQUEIDENTIFIER		NOT NULL,
	IDUSUARIO				UNIQUEIDENTIFIER		NOT NULL,
	NOME					NVARCHAR(50)			NOT NULL UNIQUE,
	DESCRICAO				NVARCHAR(100)			NOT NULL,
	PRIMARY KEY(IDCARTEIRA)
)

CREATE TABLE OPERACAO (
	IDOPERACAO				UNIQUEIDENTIFIER		NOT NULL,
	IDCARTEIRA				UNIQUEIDENTIFIER		NOT NULL,
	IDTIPOOPERACAO				UNIQUEIDENTIFIER		NOT NULL,
	IDTIPOATIVO				UNIQUEIDENTIFIER		NOT NULL,
	NOMEATIVO				NVARCHAR(50)			NOT NULL,
	SIGLAATIVO				NVARCHAR(50)			NOT NULL,
	DESCRICAOATIVO				NVARCHAR(100)			NOT NULL,
	DATAOPERACAO				DATETIME			NOT NULL,
	PRECOATIVO				INT				NOT NULL,
	QUANTIDADEATIVO				INT				NOT NULL,
	TOTAL					INT				NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY(IDCARTEIRA) REFERENCES CARTEIRA(IDCARTEIRA),
	FOREIGN KEY(IDTIPOATIVO) REFERENCES TIPOATIVO(IDTIPOATIVO),
	FOREIGN KEY(IDTIPOOPERACAO) REFERENCES TIPOOPERACAO(IDTIPOOPERACAO),
)