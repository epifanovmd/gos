import main

#Создание таблицы Groups
def CreateTableGroups():
    zapros = 'CREATE TABLE Groups (Id INTEGER PRIMARY KEY AUTOINCREMENT,' \
                                  'Cource INT NOT NULL, ' \
                                  'Name CHAR(50) NOT NULL UNIQUE);'
    main.c.execute(zapros)
    main.conn.commit()

#Создание таблицы Students
def CreateTableStudents():
    zapros = 'CREATE TABLE Students (Id INTEGER PRIMARY KEY AUTOINCREMENT,' \
                                    'LastName CHAR(20) NOT NULL, ' \
                                    'FirstName CHAR(20) NOT NULL, ' \
                                    'SecondName CHAR(20), ' \
                                    'GroupId INTEGER NOT NULL, ' \
                                    'FOREIGN KEY(GroupId) REFERENCES Groups(Id));'
    main.c.execute(zapros)
    main.conn.commit()

#Создание таблицы Disciplines
def CreateTableDisciplines():
    zapros = 'CREATE TABLE Disciplines (Id INTEGER PRIMARY KEY AUTOINCREMENT,' \
                                       'Name CHAR(30) NOT NULL UNIQUE);'
    main.c.execute(zapros)
    main.conn.commit()

#Создание таблицы Grades
def CreateTableGrades():
    zapros = 'CREATE TABLE Grades (Id INTEGER PRIMARY KEY AUTOINCREMENT,' \
                                  'Mark SMALLINT NOT NULL, ' \
                                  'StudentId INTEGER NOT NULL, ' \
                                  'DisciplineId INTEGER NOT NULL,' \
                                  'FOREIGN KEY(StudentId) REFERENCES Students(Id),' \
                                  'FOREIGN KEY(DisciplineId) REFERENCES Disciplines(Id));'
    main.c.execute(zapros)
    main.conn.commit()

#Удаление выбранной таблицы из базы
def DropTable(table):
    main.c.execute('DROP TABLE '+ table)
    main.conn.commit()

