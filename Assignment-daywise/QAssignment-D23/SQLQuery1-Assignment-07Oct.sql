
--Creating the database for the Assignment
CREATE DATABASE Assignment07Oct
USE Assignment07Oct

------------------------------------------------------------------------------
--EMP (empno - primary key, empname, salary, deptname - 
--references entries in a deptname of department table with null constraint,
--bossno - references entries in an empno of emp table with null constraint)

CREATE TABLE EMP (
    empno INT  constraint pk_employee_ID primary key,
    empname VARCHAR(100) NOT NULL,
    salary DECIMAL(10, 2) NOT NULL,
    bossno INT constraint fk_empno foreign key references EMP(empno), 
)

ALTER TABLE EMP
drop column deptname

ALTER TABLE EMP
add deptname varchar(100) constraint fk_deptname foreign key references DEPARTMENT(deptname) NULL 

DROP TABLE EMP
SELECT * FROM EMP
sp_help EMP

---------------------------------------------------------------------------------------------

--Inserting all datas 
--Into EMP

INSERT INTO EMP (empno, empname, salary, bossno) VALUES
(1, 'Alice', 75000, NULL),
(2, 'Ned', 45000, 1),
(3, 'Andrew', 25000, 2),
(4, 'Clare', 22000, 2),
(5, 'Todd', 38000, 1),
(6, 'Nancy', 22000, 5),
(7, 'Brier', 43000, 1),
(8, 'Sarah', 56000, 7),
(9, 'Sophile', 35000, 1),
(10, 'Sanjay', 15000, 3),
(11, 'Rita', 15000, 4),
(12, 'Gigi', 16000, 4),
(13, 'Maggie', 11000, 4),
(14, 'Paul', 15000, 3),
(15, 'James', 15000, 3),
(16, 'Pat', 15000, 3),
(17, 'Mark', 15000, 3);


UPDATE EMP SET deptname = 'Management' WHERE empno = 1;
UPDATE EMP SET deptname = 'Marketing' WHERE empno IN (2, 3, 4);
UPDATE EMP SET deptname = 'Accounting' WHERE empno IN (5, 6);
UPDATE EMP SET deptname = 'Purchasing' WHERE empno IN (7, 8);
UPDATE EMP SET deptname = 'Personnel' WHERE empno = 9;
UPDATE EMP SET deptname = 'Navigation' WHERE empno = 10;
UPDATE EMP SET deptname = 'Books' WHERE empno = 11;
UPDATE EMP SET deptname = 'Clothes' WHERE empno IN (12, 13);
UPDATE EMP SET deptname = 'Equipment' WHERE empno IN (14, 15);
UPDATE EMP SET deptname = 'Furniture' WHERE empno = 16;
UPDATE EMP SET deptname = 'Recreation' WHERE empno = 17;

-----------------------------------------------------------------------------------------------

--DEPARTMENT (deptname - primary key, floor, phone, empno - references entries in an empno of emp table not null)

CREATE TABLE DEPARTMENT (
    deptname VARCHAR(100)  constraint pk_dept_name primary key,
    Deptfloor VARCHAR(10) ,
	Deptphone varchar(15),
	MgrId INT constraint fk_emp foreign key references EMP(empno) 
)

ALTER TABLE DEPARTMENT
drop column empno

ALTER TABLE DEPARTMENT
drop Constraint fk_emp

SELECT * FROM DEPARTMENT
sp_help DEPARTMENT
DROP TABLE DEPARTMENT

---------------------------------------------------------------------------------------------

--Inserting all datas 
--Into DEPARTMENT

INSERT INTO DEPARTMENT (deptname, deptfloor, deptphone, mgrId) VALUES
('Management', 5, 34, 1),
('Books', 1, 81, 4),
('Clothes', 2, 24, 4),
('Equipment', 3, 57, 3),
('Furniture', 4, 14, 3),
('Navigation', 1, 41, 3),
('Recreation', 2, 29, 4),
('Accounting', 5, 35, 5),
('Purchasing', 5, 36, 7),
('Personnel', 5, 37, 9),
('Marketing', 5, 38, 2);

---------------------------------------------------------------------------------------------------

--SALES (salesno - primary key, saleqty,
--itemname -references entries in a itemname of item table with not null
--constraint, deptname - references entries in a deptname of department table with not null constraint)

CREATE TABLE SALES (
    salesno INT  constraint pk_salesno primary key,
    saleqty INT NOT NULL,
    --itemname 
    deptname varchar(100) constraint fk_deptname_sales foreign key references DEPARTMENT(deptname)   NOT NULL
)

ALTER TABLE SALES
add itemname varchar(100) constraint fk_itemname foreign key references ITEM(itemname)

DROP TABLE SALES
SELECT * FROM SALES
sp_help SALES

---------------------------------------------------------------------------------------------

--Inserting all datas 
--Into Sales

INSERT INTO SALES (Salesno, Saleqty, itemname, Deptname) VALUES
(101, 2, 'Boots-snake proof', 'Clothes'),
(102, 1, 'Pith Helmet', 'Clothes'),
(103, 1, 'Sextant', 'Navigation'),
(104, 3, 'Hat-polar Explorer', 'Clothes'),
(105, 5, 'Pith Helmet', 'Equipment'),
(106, 2, 'Pocket Knife-Nile', 'Clothes'),
(107, 3, 'Pocket Knife-Nile', 'Recreation'),
(108, 1, 'Compass', 'Navigation'),
(109, 2, 'Geo positioning system', 'Navigation'),
(110, 5, 'Map Measure', 'Navigation'),
(111, 1, 'Geo positioning system', 'Books'),
(112, 1, 'Sextant', 'Books'),
(113, 3, 'Pocket Knife-Nile', 'Books'),
(114, 1, 'Pocket Knife-Nile', 'Navigation'),
(115, 1, 'Pocket Knife-Nile', 'Equipment'),
(116, 1, 'Sextant', 'Clothes'),
--(117, 1, '', 'Equipment'),          -- Empty itemname
--(118, 1, '', 'Recreation'),         -- Empty itemname
--(119, 1, '', 'Furniture'),          -- Empty itemname
--(120, 1, 'Pocket Knife-Nile', ''),  -- Empty Deptname
(121, 1, 'Exploring in 10 easy lessons', 'Books'),
--(122, 1, 'How to win foreign friends', ''),  -- Empty itemname
--(123, 1, 'Compass', ''),          -- Empty Deptname
--(124, 1, 'Pith Helmet', ''),      -- Empty Deptname
(125, 1, 'Elephant Polo stick', 'Recreation'),
(126, 1, 'Camel Saddle', 'Recreation');


--Throwing error bcoz ItemName is not null
INSERT INTO SALES (Salesno, Saleqty, itemname, Deptname) VALUES
(123, 1, '', 'Recreation')     -- Empty ItemName

--Throwing error bcoz Deptname is not null
INSERT INTO SALES (Salesno, Saleqty, itemname, Deptname) VALUES
(124, 1, 'Pith Helmet', '')     -- Empty Deptname

------------------------------------------------------------------------------------------------
--ITEM (itemname - primary key, itemtype, itemcolor)

CREATE TABLE ITEM (
    itemname VARCHAR(100)  constraint pk_item_name primary key NOT NULL ,
    itemtype VARCHAR(15) ,
	itemcolor varchar(15),
	
)

DROP TABLE ITEM
SELECT * FROM ITEM
sp_help ITEM

---------------------------------------------------------------------------------------------

--Inserting all datas 
--Into Item



INSERT INTO ITEM (itemname, itemtype, itemcolor) VALUES
('Pocket Knife-Nile', 'E', 'Brown'),
('Pocket Knife-Avon', 'E', 'Brown'),
('Compass', 'N', NULL),                 -- NULL for itemcolor
('Geo positioning system', 'N', NULL),  -- NULL for itemcolor
('Elephant Polo stick', 'R', 'Bamboo'),
('Camel Saddle', 'R', 'Brown'),
('Sextant', 'N', NULL),                   -- NULL for itemcolor
('Map Measure', 'N', NULL),               -- NULL for itemcolor
('Boots-snake proof', 'C', 'Green'),
('Pith Helmet', 'C', 'Khaki'),
('Hat-polar Explorer', 'C', 'White'),
('Exploring in 10 Easy Lessons', 'B', NULL),  -- NULL for itemcolor
('Hammock', 'F', 'Khaki'),
('How to win Foreign Friends', 'B', NULL),     -- NULL for itemcolor
('Map case', 'E', 'Brown'),
('Safari Chair', 'F', 'Khaki'),
('Safari cooking kit', 'F', 'Khaki'),
('Stetson', 'C', 'Black'),
('Tent - 2 person', 'F', 'Khaki'),
('Tent - 8 person', 'F', 'Khaki');


--------------------------------------------------------------------------------------------------
--Selecting All Tables

SELECT * FROM ITEM
SELECT * FROM SALES
SELECT * FROM EMP
SELECT * FROM DEPARTMENT

----------------------------------------------------------------------------------------

-- identify the inserts, deletes and updates that are not possible as well

-- These Insertion in Sales table is not possible 
-- The Item name and Dept name cannot be empty as they are not null & fk

--(117, 1, '', 'Equipment'),          -- Empty itemname
--(118, 1, '', 'Recreation'),         -- Empty itemname
--(119, 1, '', 'Furniture'),          -- Empty itemname
--(120, 1, 'Pocket Knife-Nile', ''),  -- Empty Deptname
--(122, 1, 'How to win foreign friends', ''),  -- Empty itemname
--(123, 1, 'Compass', ''),          -- Empty Deptname
--(124, 1, 'Pith Helmet', ''),      -- Empty Deptname

-- The Deletes that are not possible are the one in which there is 
-- foreign key references meaning a parent Table data cannot be
-- deleted without the reference of child Table data.

