INSERT INTO Pessoas (nome, email, senha, cpf, cep, numero, ativo, TipoPessoa) VALUES
('João Silva', 'joao.silva@email.com', 'senha123', '12345678901', '12345-678', 10, 1, 'C'),
('Maria Oliveira', 'maria.oliveira@email.com', 'senha456', '98765432100', '23456-789', 20, 1, 'A'),
('Carlos Souza', 'carlos.souza@email.com', 'senha789', '11122233344', '34567-890', 30, 1, 'T');

INSERT INTO Clientes (id_pessoa) VALUES
(1);

INSERT INTO Administradores (id_pessoa) VALUES
(2);

INSERT INTO Tecnicos (id_pessoa) VALUES
(3);

INSERT INTO Telefones (id_pessoa, numero) VALUES
(1, '1111111111'),
(2, '2222222222'),
(3, '3333333333');

INSERT INTO Ambientes (nomeambiente) VALUES
('Sala'),
('Cozinha'),
('Quarto');

INSERT INTO TipoDispositivos (nometipodispositivo) VALUES
('Sensor de Movimento'),
('Câmera de Segurança'),
('Termostato');

INSERT INTO Dispositivos (nomedispositivo, id_ambiente, porta, id_cliente, id_tecnico, id_tipodispositivo) VALUES
('Sensor de Movimento 1', 1, 8080, 1, 3, 1),
('Câmera de Segurança 1', 2, 8081, 1, 3, 2),
('Termostato 1', 3, 8082, 1, 3, 3);

INSERT INTO RelatorioInstalacoes (id_cliente, id_tecnico, id_dispositivo, data_instalacao) VALUES
(1, 3, 1, '2024-01-01 10:00:00'),
(1, 3, 2, '2024-01-02 11:00:00'),
(1, 3, 3, '2024-01-03 12:00:00');

INSERT INTO GrupoAmbientes (nomegrupoambiente, id_cliente) VALUES
('Grupo 1', 1);

INSERT INTO AmbienteGrupos (id_grupo, id_ambiente) VALUES
(1, 1),
(1, 2),
(1, 3);
