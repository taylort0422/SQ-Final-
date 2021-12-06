DROP DATABASE IF EXISTS `tms`;
CREATE DATABASE IF NOT EXISTS `tms` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `tms`;

-- MySQL dump 10.13  Distrib 8.0.26, for Win64 (x86_64)
--
-- Host: localhost    Database: tms
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

DROP TABLE IF EXISTS tms.`carrier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE tms.`carrier` (
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

LOCK TABLES tms.`carrier` WRITE;
/*!40000 ALTER TABLE tms.`carrier` DISABLE KEYS */;
INSERT INTO tms.`carrier` VALUES (1,'Planet Express','Windsor',48,287,5.21,0.3621,0.08),(2,'Planet Express','Hamilton',50,640,5.21,0.3621,0.08),(3,'Planet Express','Oshawa',50,640,5.21,0.3621,0.08),(4,'Planet Express','Belleville',48,640,5.21,0.3621,0.08),(5,'Planet Express','Ottawa',50,640,5.21,0.3621,0.08),(6,'Schooner\'s','London',16,14,5.05,0.3434,0.07),(7,'Schooner\'s','Toronto',14,98,5.05,0.3434,0.07),(8,'Schooner\'s','Kingston',18,98,5.05,0.3434,0.07),(9,'Tillman Transport','Windsor',24,35,5.11,0.3012,0.09),(10,'Tillman Transport','London',18,3,5.11,0.3012,0.09),(11,'Tillman Transport','Hamilton',18,3,5.11,0.3012,0.09),(12,'We Haul','Ottawa',11,0,5.2,0,0.065),(13,'We Haul','Toronto',11,0,5.2,0,0.065);
/*!40000 ALTER TABLE tms.`carrier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `citylist`
--

DROP TABLE IF EXISTS tms.`citylist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE tms.`citylist` (
  `CityID` int NOT NULL AUTO_INCREMENT,
  `WhichOrder` int DEFAULT NULL,
  `Carrier` varchar(45) DEFAULT NULL,
  `DepotID` int NOT NULL,
  PRIMARY KEY (`CityID`),
  KEY `WhichOrder_idx` (`WhichOrder`),
  CONSTRAINT `WhichOrder` FOREIGN KEY (`WhichOrder`) REFERENCES tms.`order` (`OrderID`)
) ENGINE=InnoDB AUTO_INCREMENT=197 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `citylist`
--

LOCK TABLES tms.`citylist` WRITE;
/*!40000 ALTER TABLE tms.`citylist` DISABLE KEYS */;
/*!40000 ALTER TABLE tms.`citylist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS tms.`customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE tms.`customer` (
  `CustomerID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer`
--

LOCK TABLES tms.`customer` WRITE;
/*!40000 ALTER TABLE tms.`customer` DISABLE KEYS */;
/*!40000 ALTER TABLE tms.`customer` ENABLE KEYS */;
UNLOCK TABLES;


--
-- Table structure for table `invoice`
--

DROP TABLE IF EXISTS tms.`invoice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE tms.`invoice` (
  `InvoiceID` int NOT NULL AUTO_INCREMENT,
  `OrderID` int DEFAULT NULL,
  `Amount` float DEFAULT NULL,
  `invoiceFile` blob,
  PRIMARY KEY (`InvoiceID`),
  KEY `OrderID_idx` (`OrderID`),
  CONSTRAINT `OrderID` FOREIGN KEY (`OrderID`) REFERENCES tms.`order` (`OrderID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `invoice`
--

LOCK TABLES tms.`invoice` WRITE;
/*!40000 ALTER TABLE tms.`invoice` DISABLE KEYS */;
/*!40000 ALTER TABLE tms.`invoice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ltltruck`
--

DROP TABLE IF EXISTS tms.`ltltruck`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE tms.`ltltruck` (
  `LTLTruckID` int NOT NULL,
  `LTLID` varchar(45) DEFAULT NULL,
  `MaxLoad` varchar(45) DEFAULT NULL,
  `CurrentLoad` varchar(45) DEFAULT NULL,
  KEY `LTLTruckID_idx` (`LTLTruckID`),
  CONSTRAINT `LTLTruckID` FOREIGN KEY (`LTLTruckID`) REFERENCES tms.`truck` (`TruckID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ltltruck`
--

LOCK TABLES tms.`ltltruck` WRITE;
/*!40000 ALTER TABLE tms.`ltltruck` DISABLE KEYS */;
/*!40000 ALTER TABLE tms.`ltltruck` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS tms.`order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE tms.`order` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `CustomerID` int DEFAULT NULL,
  `UserID` int DEFAULT NULL,
  `OrderType` int DEFAULT NULL,
  `TotalCost` float DEFAULT NULL,
  `DepartCity` varchar(45) DEFAULT NULL,
  `DestCity` varchar(45) DEFAULT NULL,
  `TotalHours` float DEFAULT NULL,
  `Surcharge` float DEFAULT NULL,
  `Markup` float DEFAULT NULL,
  `VanType` float DEFAULT NULL,
  `OrderConfirmed` tinyint(1) DEFAULT NULL,
  `OrderDate` datetime DEFAULT NULL,
  `orderSize` int DEFAULT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `CustomerID_idx` (`CustomerID`),
  KEY `UserID_idx` (`UserID`),
  CONSTRAINT `CustomerID` FOREIGN KEY (`CustomerID`) REFERENCES tms.`customer` (`CustomerID`),
  CONSTRAINT `UserID` FOREIGN KEY (`UserID`) REFERENCES tms.`user` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES tms.`order` WRITE;
/*!40000 ALTER TABLE tms.`order` DISABLE KEYS */;
/*!40000 ALTER TABLE tms.`order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `route`
--

DROP TABLE IF EXISTS tms.`route`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE tms.`route` (
  `RouteID` int NOT NULL AUTO_INCREMENT,
  `DepartLocation` varchar(45) DEFAULT NULL,
  `DestinationLocation` varchar(45) DEFAULT NULL,
  `Hours` float DEFAULT NULL,
  `KMs` float DEFAULT NULL,
  `Direction` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`RouteID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `route`
--

LOCK TABLES tms.`route` WRITE;
/*!40000 ALTER TABLE `route` DISABLE KEYS */;
INSERT INTO tms.`route` VALUES (1,'Windsor','London',2.5,191,'East'),(2,'London','Hamilton',1.75,127,'East'),(3,'Hamilton','Toronto',1.25,68,'East'),(4,'Toronto','Oshawa',1.3,60,'East'),(5,'Oshawa','Belleville',1.65,134,'East'),(6,'Belleville','Kingston',1.2,82,'East'),(7,'Kingston','Ottawa',2.5,196,'East'),(8,'Ottawa','Kingston',2.5,196,'West'),(9,'Kingston','Belleville',1.2,82,'West'),(10,'Belleville','Oshawa',1.65,134,'West'),(11,'Oshawa','Toronto',1.3,60,'West'),(12,'Toronto','Hamilton',1.25,68,'West'),(13,'Hamilton','London',1.75,127,'West'),(14,'London','Windsor',2.5,191,'West');
/*!40000 ALTER TABLE tms.`route` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trip`
--

DROP TABLE IF EXISTS tms.`trip`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE tms.`trip` (
  `TripID` int NOT NULL AUTO_INCREMENT,
  `OrderID` int DEFAULT NULL,
  `CarrierID` int DEFAULT NULL,
  `TripCost` varchar(45) DEFAULT NULL,
  `GrossCost` varchar(45) DEFAULT NULL,
  `Hours` float DEFAULT NULL,
  `Kilometers` float DEFAULT NULL,
  `OriginCity` varchar(45) DEFAULT NULL,
  `DestinationCity` varchar(45) DEFAULT NULL,
  `OriginDepotID` int DEFAULT NULL,
  `DestinationDepotID` int DEFAULT NULL,
  PRIMARY KEY (`TripID`),
  KEY `OrderID_idx` (`OrderID`),
  KEY `OriginDepotID_idx` (`OriginDepotID`),
  KEY `DestinationDepotID_idx` (`DestinationDepotID`),
  CONSTRAINT `DestinationDepotID` FOREIGN KEY (`DestinationDepotID`) REFERENCES tms.`carrier` (`CarrierID`),
  CONSTRAINT `OriginDepotID` FOREIGN KEY (`OriginDepotID`) REFERENCES tms.`carrier` (`CarrierID`),
  CONSTRAINT `TripOrderID` FOREIGN KEY (`OrderID`) REFERENCES tms.`order` (`OrderID`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trip`
--

LOCK TABLES tms.`trip` WRITE;
/*!40000 ALTER TABLE tms.`trip` DISABLE KEYS */;
/*!40000 ALTER TABLE tms.`trip` ENABLE KEYS */;
UNLOCK TABLES;



CREATE USER 'tmsUser'@'localhost' IDENTIFIED BY 'pass123';
GRANT SELECT, INSERT, UPDATE, DELETE ON tms.* TO 'tmsUser'@'localhost'
