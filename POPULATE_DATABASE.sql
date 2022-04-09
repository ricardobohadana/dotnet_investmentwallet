INSERT INTO TIPOOPERACAO(IDTIPOOPERACAO, NOME)
VALUES ('6daefdb9-f6b0-4556-8486-10b150bca0c6', 'Compra'), ('4096f1c7-75ff-4c29-9410-879c77f8cfbd', 'Venda')
GO


INSERT INTO TIPOATIVO(IDTIPOATIVO, NOME)
VALUES ('02215fbd-1ccd-4e44-87f7-2ee1ad31720a', 'AÇÃO'), ('b2476190-6c8b-47b3-8543-da58c0f6127a', 'RENDA FIXA'),
	   ('cbd745a4-a252-403f-9aaf-fac188c2f744', 'CRIPTOMOEDAS'), ('5309f358-b6f8-4f53-8db0-9c2f6fd27253', 'FUNDOS IMOBILIÁRIOS'),
	   ('278f5ec1-ac8f-479a-a07a-699551e6eba4', 'FUNDOS DE INVESTIMENTO')


INSERT INTO PERFILINVESTIDOR(ID, TIPO, DESCRICAO)
VALUES	('010ba65f-be13-444e-9fe9-4424dc214181', 'Conservador', 'Prefere a segurança do seu patrimônio acima da rentabilidade.'),
		('d2693935-a641-4410-bf79-c95358eccdd6', 'Moderado', 'Mantém forte interesse pela segurança, mas está disposta a abrir mão de parte dela às vezes para ter retornos melhores.'),
		('6170d630-0028-46b6-a98f-ffc3ce6d3290', 'Agressivo', 'Tem alta tolerância a riscos e baixa necessidade de liquidez em curto ou médio prazo.')