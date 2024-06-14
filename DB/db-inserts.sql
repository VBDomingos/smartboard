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


Insert into TipoDispositivos values ('Lâmpada')
Insert into TipoDispositivos values ('Temporizador')
Insert into TipoDispositivos values ('Alarme')

 INSERT INTO Pessoas (nome, email, senha, cpf, cep, numero, ativo, TipoPessoa) VALUES ('Admin', 'admin@admin.com', '123456', '11111111111', '11111111', 111, 1, 'A');
                DECLARE @id_pessoa INT; SET @id_pessoa = SCOPE_IDENTITY();
                INSERT INTO Administradores(id_pessoa) VALUES (@id_pessoa);
                INSERT INTO telefones (id_pessoa, numero) Values (@id_pessoa, 17991111111);

  INSERT INTO Pessoas (nome, email, senha, cpf, cep, numero, ativo, TipoPessoa) VALUES ('Tecnico', 'tecnico@tecnico.com', '123456', '22222222222', '22222222', 222, 1, 'T');
                DECLARE @id_pessoa INT; SET @id_pessoa = SCOPE_IDENTITY();
                INSERT INTO Tecnicos(id_pessoa) VALUES (@id_pessoa);
                INSERT INTO telefones (id_pessoa, numero) Values (@id_pessoa, 17991111111);