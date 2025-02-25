DROP DATABASE  IF EXISTS bddpersonnels;
CREATE DATABASE IF NOT EXISTS bddpersonnels;
USE bddpersonnels;
--
-- Base de données :  `bddpersonnels`
--

CREATE TABLE `fonctions` (
  `id` int(11) NOT NULL,
  `intitule` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `personnels` (
  `id` int(11) NOT NULL,
  `prenom` text CHARACTER SET latin1 NOT NULL,
  `nom` text CHARACTER SET latin1 NOT NULL,
  `idService` int(11) NOT NULL,
  `idFonction` int(11) NOT NULL,
  `telephone` text CHARACTER SET latin1,
  `photo` longblob
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;


CREATE TABLE `services` (
  `id` int(11) NOT NULL,
  `intitule` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE `fonctions`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_UNIQUE` (`id`),
  ADD UNIQUE KEY `id_INTITULE` (`intitule`);

ALTER TABLE `personnels`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_UNIQUE` (`id`),
  ADD KEY `idService` (`idService`),
  ADD KEY `idFonction` (`idFonction`);

ALTER TABLE `services`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id_UNIQUE` (`id`),
  ADD UNIQUE KEY `id_INTITULE` (`intitule`);

ALTER TABLE `fonctions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `personnels`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `services`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `personnels`
  ADD CONSTRAINT `affect_ibfk_1` FOREIGN KEY (`idService`) REFERENCES `services` (`id`),
  ADD CONSTRAINT `affect_ibfk_2` FOREIGN KEY (`idFonction`) REFERENCES `fonctions` (`id`);

USE mysql;
GRANT ALL PRIVILEGES ON bddpersonnels. * TO 'GestionnaireBDD'@'%' IDENTIFIED BY 'Password1234@but';
FLUSH PRIVILEGES;

Use mysql;
GRANT  SELECT ON bddpersonnels. * TO 'UtilisateurBDD'@'%' IDENTIFIED BY 'Password1234@';
FLUSH PRIVILEGES