https://github.com/user-attachments/assets/d6dea0fd-801f-4f60-afef-6fd6f53b1e06

Sistema para agendamento de quartos para a Pousada Palimar 
(Dobrada-SP)


Este sistema foi desenvolvido com o propósito de substituir o cadastro manual realizado pelos funcionários da Pousada Palimar. 
Anteriormente, o registro de usuários e o agendamento de quartos eram feitos manualmente através de uma grande agenda, resultando em um processo ineficiente. 
Em parceria com um dos desenvolvedores, que possuía uma relação próxima tanto conosco (desenvolvedores) quanto com os funcionários e proprietários da pousada, 
decidimos oferecer e criar juntos um sistema web para melhorar esses processos.

Utilizamos diversas tecnologias para criar o sistema.Foram utilizadas para desenvolver o sistema, a IDE Visual Studio Community pela sua robusta quantidade de recursos
e a linguagem C# pela sua eficiencia e relevancia no mercado.
e interação com o banco de dados. Adotamos o padrão de design MVC para organizar modelos, visualizações e controladores de forma estruturada. Além disso, implementamos dois frameworks essenciais:
o Microsoft Entity Framework, para criar o modelo do banco de dados e realizar migrações conforme necessário, e o Microsoft Identity, para gerenciar cadastros, autenticação e autorização.

A autorização no sistema foi dividida em dois níveis: cliente e funcionário. Os clientes podem apenas realizar reservas, escolhendo o tipo de quarto, suas datas de check-in (entrada) e check-out (saída), 
que bloqueiam o quarto para outros usuários que escolham o mesmo. Já os funcionários têm permissões adicionais, como editar, adicionar, excluir e visualizar quartos e reservas, além de poderem
alterar as permissões de outros usuários.
Como banco de dados, optamos pelo SQL Server devido à sua confiabilidade e segurança.
Este projeto foi desenvolvido em cerca de 6 meses.

Integrantes e seus cargos:

Alexandre José Peixoto dos Santos (Front-end, Back-end e Gestor da equipe)
Linkedin:

Arthur Americo Durante (Front-end e Project Owner)
Linkedin:

Fernando Felipe dos Santos (Front-end)
Linkedin:
