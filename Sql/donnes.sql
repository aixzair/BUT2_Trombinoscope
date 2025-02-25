delete from personnels;
delete from fonctions;
delete from services;

insert into fonctions (`id`, `intitule`) values
(1, 'patron'),
(2, 'developpeur'),
(3, 'analyste'),
(4, 'designer'),
(5, 'support');

insert into services (`id`, `intitule`) values
(1, 'financier'),
(2, 'publicité'),
(3, 'informatique'),
(4, 'ressources humaines'),
(5, 'logistique');

insert into personnels (`id`, `prenom`, `nom`, `idservice`, `idFonction`, `telephone`, `photo`) values
(1, 'Tom', 'Motorola', 1, 1, '0611121314', null),
(2, 'Lea', 'Aliba', 2, 2, '0621222324', null),
(3, 'Alice', 'Ciali', 3, 3, '0631323334', null),
(4, 'Leo', 'Oleo', 4, 4, '0641424344', null),
(5, 'Max', 'Xim', 5, 5, '0651525354', null),
(6, 'Eva', 'Ave', 1, 4, '0661626364', null),
(7, 'George', 'Gorg', 2, 3, '0671727374', null),
(8, 'Tomas', 'Motas', 3, 2, '0681828384', null),
(9, 'Lucas', 'Saul', 4, 1, '0691929394', null),
(10, 'Ralf', 'Bri', 1, 3, '0601020304', null);
