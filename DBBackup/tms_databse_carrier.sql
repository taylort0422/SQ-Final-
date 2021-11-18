-- MySQL dump 10.13  Distrib 8.0.26, for Win64 (x86_64)
--
-- Host: localhost    Database: tms_databse
-- ------------------------------------------------------
-- Server version	8.0.26

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `carrier`
--

DROP TABLE IF EXISTS `carrier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carrier` (
  `CarrierID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `DepotCity` varchar(45) DEFAULT NULL,
  `FTLA` int DEFAULT NULL,
  `LTLA` int DEFAULT NULL,
  `FTLRate` float DEFAULT NULL,
  `LTLRate` float DEFAULT NULL,
  `ReefCharge` float DEFAULT NULL,
  PRIMARY KEY (`CarrierID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carrier`
--

LOCK TABLES `carrier` WRITE;
/*!40000 ALTER TABLE `carrier` DISABLE KEYS */;
INSERT INTO `carrier` VALUES (1,'Planet Express','Windsor',50,640,5.21,0.3621,0.08),(2,'Planet Express','Hamilton',50,640,5.21,0.3621,0.08),(3,'Planet Express','Oshawa',50,640,5.21,0.3621,0.08),(4,'Planet Express','Belleville',50,640,5.21,0.3621,0.08),(5,'Planet Express','Ottawa',50,640,5.21,0.3621,0.08),(6,'Schooner\'s','London',18,98,5.05,0.3434,0.07),(7,'Schooner\'s','Toronto',18,98,5.05,0.3434,0.07),(8,'Schooner\'s','Kingston',18,98,5.05,0.3434,0.07),(9,'Tillman Transport','Windsor',24,35,5.11,0.3012,0.09),(10,'Tillman Transport','London',18,45,5.11,0.3012,0.09),(11,'Tillman Transport','Hamilton',18,45,5.11,0.3012,0.09),(12,'We Haul','Ottawa',11,0,5.2,0,0.065),(13,'We Haul','Toronto',11,0,5.2,0,0.065);
/*!40000 ALTER TABLE `carrier` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-11-18 15:56:57
