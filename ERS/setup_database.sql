-- ===========================================================
-- 2C Rentals - Database Setup Script
-- Run this in MySQL before launching the application
-- ===========================================================

CREATE DATABASE IF NOT EXISTS twoc_rentals_db;
USE twoc_rentals_db;

-- Customers
CREATE TABLE IF NOT EXISTS customers (
    customer_id   INT AUTO_INCREMENT PRIMARY KEY,
    full_name     VARCHAR(150) NOT NULL,
    contact_no    VARCHAR(30)  NOT NULL,
    created_at    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- Equipment catalog
CREATE TABLE IF NOT EXISTS equipment (
    equipment_id  INT AUTO_INCREMENT PRIMARY KEY,
    name          VARCHAR(150) NOT NULL,
    category      VARCHAR(50)  NOT NULL,
    daily_rate    DECIMAL(10,2) NOT NULL,
    total_stock   INT NOT NULL DEFAULT 0,
    avail_stock   INT NOT NULL DEFAULT 0,
    icon_tag      VARCHAR(10)  NOT NULL DEFAULT '📦',
    is_active     TINYINT(1)   NOT NULL DEFAULT 1
) ENGINE=InnoDB;

-- Rentals header
CREATE TABLE IF NOT EXISTS rentals (
    rental_id     INT AUTO_INCREMENT PRIMARY KEY,
    booking_code  VARCHAR(20)  NOT NULL UNIQUE,
    customer_id   INT NOT NULL,
    rental_start  DATE NOT NULL,
    rental_end    DATE NOT NULL,
    security_dep  DECIMAL(10,2) NOT NULL DEFAULT 500.00,
    subtotal      DECIMAL(10,2) NOT NULL,
    total_amount  DECIMAL(10,2) NOT NULL,
    status        VARCHAR(20)  NOT NULL DEFAULT 'Active',
    created_at    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
) ENGINE=InnoDB;

-- Rental line items
CREATE TABLE IF NOT EXISTS rental_details (
    detail_id     INT AUTO_INCREMENT PRIMARY KEY,
    rental_id     INT NOT NULL,
    equipment_id  INT NOT NULL,
    quantity      INT NOT NULL,
    daily_rate    DECIMAL(10,2) NOT NULL,
    days_rented   INT NOT NULL,
    line_total    DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (rental_id)   REFERENCES rentals(rental_id),
    FOREIGN KEY (equipment_id) REFERENCES equipment(equipment_id)
) ENGINE=InnoDB;

-- Admin accounts
CREATE TABLE IF NOT EXISTS admins (
    admin_id      INT AUTO_INCREMENT PRIMARY KEY,
    username      VARCHAR(50)  NOT NULL UNIQUE,
    password_hash VARCHAR(64)  NOT NULL,
    full_name     VARCHAR(150) NOT NULL
) ENGINE=InnoDB;

-- ===================== SEED DATA =====================

-- Equipment
INSERT INTO equipment (name, category, daily_rate, total_stock, avail_stock, icon_tag) VALUES
('Executive Chair',    'Seating',      250.00, 20, 20, '🪑'),
('Banquet Table',      'Tables',       450.00, 15, 15, '🍴'),
('PA System',          'Audio/Visual', 1200.00, 5,  5,  '🎵'),
('4K Projector',       'Audio/Visual', 2500.00, 3,  3,  '📽'),
('Outdoor Umbrella',   'Tables',       350.00, 10, 10, '☂'),
('Stage Lighting Kit', 'Audio/Visual', 800.00,  4,  4,  '💡');

-- Default admin  (password = admin123, SHA-256)
INSERT INTO admins (username, password_hash, full_name) VALUES
('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'System Admin');
