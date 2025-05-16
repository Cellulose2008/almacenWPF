-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 16, 2025 at 06:25 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `almacen`
--

-- --------------------------------------------------------

--
-- Table structure for table `almacenista`
--

CREATE TABLE `almacenista` (
  `PKalmacenista` int(100) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `contra` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `almacenista`
--

INSERT INTO `almacenista` (`PKalmacenista`, `nombre`, `contra`) VALUES
(1, 'Juan', 'cisco123'),
(2, 'Carlos', 'cisco123'),
(3, 'Bodoque', 'cisco123');

-- --------------------------------------------------------

--
-- Table structure for table `piezas`
--

CREATE TABLE `piezas` (
  `PKpiezas` int(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `estatus` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `piezas`
--

INSERT INTO `piezas` (`PKpiezas`, `nombre`, `estatus`) VALUES
(1, 'Destornillador', 'PRESTADO'),
(2, 'Taladro', 'EN ALMACÉN'),
(3, 'Cinta Métrica', 'EN ALMACÉN'),
(4, 'Martillo', 'PRESTADO'),
(5, 'Escalera', 'PRESTADO');

-- --------------------------------------------------------

--
-- Table structure for table `tecnico`
--

CREATE TABLE `tecnico` (
  `PKtecnico` int(100) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `contra` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tecnico`
--

INSERT INTO `tecnico` (`PKtecnico`, `nombre`, `contra`) VALUES
(2, 'Mike', 'cisco123'),
(3, 'Elizabeth', 'cisco123'),
(4, 'Charlie', 'cisco123');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `almacenista`
--
ALTER TABLE `almacenista`
  ADD PRIMARY KEY (`PKalmacenista`);

--
-- Indexes for table `piezas`
--
ALTER TABLE `piezas`
  ADD PRIMARY KEY (`PKpiezas`);

--
-- Indexes for table `tecnico`
--
ALTER TABLE `tecnico`
  ADD PRIMARY KEY (`PKtecnico`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `almacenista`
--
ALTER TABLE `almacenista`
  MODIFY `PKalmacenista` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `piezas`
--
ALTER TABLE `piezas`
  MODIFY `PKpiezas` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `tecnico`
--
ALTER TABLE `tecnico`
  MODIFY `PKtecnico` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
