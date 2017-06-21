import main
from random import randint

#Добавление 5 записей; номера и названия группы в таблицу Groups
def Add5DataToTableGroups():
    for i in range(1,6):
        #выполняем запрос
        main.c.execute('INSERT INTO Groups(Cource, Name) VALUES(?, ?);', (i,str(i)+'03'))
    # Сохраняем изменения в базе
    main.conn.commit()

#Добавление 25 студентов; имя, фамилия, отчество студента и id группы в таблицу Students
def Add25DataToTableStudents():
    groups = main.c.execute('SELECT Id FROM Groups').fetchall()
    k=1
    for group_id in groups:
        #Добавляем по 5 студентов в каждую группу
        for i in range(1,6):
            zapros = ('Olaf' + str(k), 'Karl'+ str(k), 'First'+ str(k), group_id[0])
            main.c.execute('INSERT INTO Students(LastName, FirstName, SecondName, GroupId) VALUES (?, ?, ?, ?);', zapros)
            k+=1
    main.conn.commit()


#Добавление 5 дисциплин в таблицу Disciplines
def Add5DataToTableDisciplines():
    main.c.execute('INSERT INTO Disciplines(Name) VALUES (?);', ['Matan'])
    main.c.execute('INSERT INTO Disciplines(Name) VALUES (?);', ['Diffur'])
    main.c.execute('INSERT INTO Disciplines(Name) VALUES (?);', ['Complex'])
    main.c.execute('INSERT INTO Disciplines(Name) VALUES (?);', ['Algorithm'])
    main.c.execute('INSERT INTO Disciplines(Name) VALUES (?);', ['Parallel'])
    main.conn.commit()

#Добавление оценки, студента и дисциплины в таблицу Grades 50 штук, оценка, студент и дисциплина заполняются рандомно
def AddToTableGrades():
    rows_st = main.c.execute('SELECT Id FROM Students').fetchall()
    rows_dis = main.c.execute('SELECT Id FROM Disciplines').fetchall()
    for i in range(0, 50):
        zapros = [randint(2, 5), rows_st[randint(0, 24)][0], rows_dis[randint(0, 4)][0]]
        main.c.execute('INSERT INTO Grades(Mark, StudentId, DisciplineId) VALUES (?, ?, ?);', zapros)
    main.conn.commit()
