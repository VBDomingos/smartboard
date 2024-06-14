-- Create database SmartBoard

use SmartBoard

-- Tabela Pessoa
CREATE TABLE Pessoas (
    id_pessoa INT identity PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    senha VARCHAR(255) NOT NULL,
    cpf VARCHAR(11) UNIQUE NOT NULL,
    cep VARCHAR(9) NOT NULL,
    numero int NOT NULL,
    ativo bit NOT NULL DEFAULT 1,
    TipoPessoa CHAR(1) CHECK (TipoPessoa In ('A', 'C', 'T')) NOT NULL
);

-- Tabela Cliente
CREATE TABLE Clientes (
    id_cliente INT identity PRIMARY KEY,
    id_pessoa INT NOT NULL,
    FOREIGN KEY (id_pessoa) REFERENCES Pessoas(id_pessoa)
);

-- Tabela Administrador
CREATE TABLE Administradores (
    id_administrador INT identity PRIMARY KEY,
    id_pessoa INT NOT NULL,
    FOREIGN KEY (id_pessoa) REFERENCES Pessoas(id_pessoa)
);

-- Tabela Tecnico
CREATE TABLE Tecnicos (
    id_tecnico INT identity PRIMARY KEY,
    id_pessoa INT NOT NULL,
    FOREIGN KEY (id_pessoa) REFERENCES Pessoas(id_pessoa)
);

-- Tabela Telefone
CREATE TABLE Telefones (
    id_telefone INT identity PRIMARY KEY,
    id_pessoa INT NOT NULL,
    numero VARCHAR(15) NOT NULL,
    FOREIGN KEY (id_pessoa) REFERENCES Pessoas(id_pessoa)
);


-- Tabela Ambiente
CREATE TABLE Ambientes (
    id_ambiente INT identity PRIMARY KEY,
    nomeambiente VARCHAR(100) NOT NULL
);

-- Tabela Tipo dispositivo
CREATE Table TipoDispositivos(
    id_tipodispositivo INT identity PRIMARY KEY,
    nometipodispositivo VARCHAR(100) NOT NULL,
)

-- Tabela Dispositivo
CREATE TABLE Dispositivos (
    id_dispositivo INT identity PRIMARY KEY,
    nomedispositivo VARCHAR(100) NOT NULL,
    id_ambiente INT NOT NULL,
    porta int NOT NULL,
    id_cliente INT NOT NULL,
    id_tecnico INT NOT NULL,
    id_tipodispositivo INT NOT NULL,
    FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente),
    FOREIGN KEY (id_ambiente) REFERENCES Ambientes(id_ambiente),
    FOREIGN KEY (id_tecnico) REFERENCES Tecnicos(id_tecnico),
    FOREIGN KEY (id_tipodispositivo) REFERENCES TipoDispositivos(id_tipodispositivo)
);

-- Tabela Relatorio
CREATE TABLE RelatorioInstalacoes (
    id_instalacao INT identity PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_tecnico INT NOT NULL,
    id_dispositivo INT NOT NULL,
    data_instalacao DATETIME NOT NULL,
    FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente),
    FOREIGN KEY (id_tecnico) REFERENCES Tecnicos(id_tecnico),
    FOREIGN KEY (id_dispositivo) REFERENCES Dispositivos(id_dispositivo)
);

-- Tabela GrupoAmbiente
CREATE TABLE GrupoAmbientes (
    id_grupo INT identity PRIMARY KEY,
    nomegrupoambiente VARCHAR(100) NOT NULL,
    id_cliente INT NOT NULL,
    FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente)
);

-- Tabela AmbienteGrupo
CREATE TABLE AmbienteGrupos (
    id_grupo INT NOT NULL,
    id_ambiente INT NOT NULL,
    PRIMARY KEY (id_grupo, id_ambiente),
    FOREIGN KEY (id_grupo) REFERENCES GrupoAmbientes(id_grupo),
    FOREIGN KEY (id_ambiente) REFERENCES Ambientes(id_ambiente)
);

-- View ClienteDispositivosView
CREATE VIEW ClienteDispositivosView AS
SELECT 
    c.id_cliente,
    p.nome AS cliente_nome,
    g.nome AS ambiente_nome,
    d.nome AS dispositivo_nome
FROM 
    Clientes c
JOIN 
    Pessoas p ON c.id_pessoa = p.id_pessoa
JOIN 
    GrupoAmbientes g ON g.id_cliente = c.id_cliente
JOIN
	AmbienteGrupos ag on ag.id_grupo = g.id_grupo
JOIN
	Ambientes a on a.id_ambiente = ag.id_ambiente
JOIN 
    Dispositivos d ON d.id_ambiente = a.id_ambiente




INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Closet');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Copa');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Corredor');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Cozinha');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Despensa');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Escritório');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Genkan');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Quarto');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Sala de Estar');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Sala de Jantar');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Sala de Reuniões');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Terraço');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Varanda');
INSERT INTO [SmartBoard].[dbo].[Ambientes] VALUES ('Vestíbulo');



-- 3. Criação da procedure de registro no relatórioInstalacoes
IF OBJECT_ID ( 'registrar_relatorioInstalacoes', 'P' ) IS NOT NULL
    DROP PROCEDURE registrar_relatorioInstalacoes;
GO
CREATE PROCEDURE registrar_relatorioInstalacoes
	@cliente_id INT,
    @tecnico_id INT,
    @dispositivo_id INT
AS
BEGIN
    INSERT INTO RelatorioInstalacoes(id_cliente, id_tecnico, id_dispositivo, data_instalacao)
    VALUES (@cliente_id, @tecnico_id, @dispositivo_id, GETDATE());
END;

-- 4. Criação da trigger para chamar a procedure após inserção na tabela de dispositivos
IF OBJECT_ID ( 'trigger_registrar_relatorioInstalacoes', 'P' ) IS NOT NULL
    DROP TRIGGER trigger_registrar_relatorioInstalacoes;
GO
CREATE TRIGGER trigger_registrar_relatorioInstalacoes
ON dispositivos
AFTER INSERT
AS
BEGIN
    DECLARE @cliente_id INT,
			@tecnico_id INT,
            @dispositivo_id INT;

    SELECT @tecnico_id = i.id_tecnico,
           @dispositivo_id = i.id_dispositivo,
		   @cliente_id = i.id_cliente
    FROM inserted i;

    EXEC registrar_relatorioInstalacoes @cliente_id, @tecnico_id, @dispositivo_id;
END;