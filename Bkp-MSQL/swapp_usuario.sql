-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: swapp
-- ------------------------------------------------------
-- Server version	5.5.5-10.1.21-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(500) NOT NULL,
  `login` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` mediumtext NOT NULL,
  `telefone` varchar(45) NOT NULL,
  `isAtivo` bit(1) NOT NULL DEFAULT b'1',
  `dataNascimento` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `cpf` varchar(15) NOT NULL,
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (2,'Pedro Correia','pedmcor@gmail.com','pedmcor@gmail.com','jm+QCdatLHwfVeE1bQHGbe88gGU3STIDRZCWxm4fpu+WeGOzgZVH8xYyIf8mOSLcNvg3hBi6ZrGSSGKJ7uCEuqAP3sw1cMnUmekjFl+LzrLpqm4epHTl2IWfqeIKmkjSP4Rjh2WLB9ah1uJbxrX2JK+rqneF0b2EM2jRamYUg6A=','41996362448','','1994-11-08 00:00:00','099453093962'),(3,'Bruno Felipe','brunofelipe10@hotmail.com','brunofelipe10@hotmail.com','N59vMxMkHxHhmDOzHFD7GZsHfJeFSodjKa1NJarl1xThygeWaEU5sqIRtGenkyuJAJamppm0K8h5hnXoNsAJbkOotcfrENHI5QTJBD8wX+9vj2NBGu8nNyFZq7wyg8Dj8vDDIoX/IZvBXgcHfXNeHJFUnxuFoGNaBO5H0POYRMQ=','41996362448','','1994-11-08 00:00:00','09953093962'),(4,'Lucas Nakamura','lucas.nakamura96@hotmail.com','lucas.nakamura96@hotmail.com','uHC9QskW4PVehgIh4VFKB+XZG2UhKXB8PX2N0PVoJ0+dqEv2grbXeSvc3wnUS2H9ujvtNhi6pZ3BfQ0CXjnnYWMcb546bPzssflllD6AcwQ6pNdqV51ynptNi6IAQRYssxwSylLOLLzRmc5fx3IN453PJbS025Ky7ROISd9LqDY=','41996362448','','1994-11-08 00:00:00','52377654401');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-06-12 23:57:08
