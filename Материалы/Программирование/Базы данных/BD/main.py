import sqlite3
from add_data_to_tables import *
from create_tables import *

#Таблицы
Students = 'Students'
Groups = 'Groups'
Disciplines = 'Disciplines'
Grades = 'Grades'

#Подключаемся к базе
conn = sqlite3.connect('Univer.db')
# Создаем курсор - это специальный объект который делает запросы и получает их результаты
c = conn.cursor()

#Удаляем все данные из выбранной таблицы
def DeleteAllFromTable(table):
    #выполняем запрос
    c.execute("DELETE FROM " + table)
    # Сохраняем изменения в базе
    conn.commit()

def PrintRows(all_rows):
    for r in all_rows:
        print(r)
    print()

#Выводим все данные из выбранной таблицы
def PrintTable(table):
    all_rows = c.execute('SELECT * FROM ' + table).fetchall()
    PrintRows(all_rows)

#Выводим таблицу Grades
def PrintTableGrades():
    all_rows = c.execute('SELECT Grades.Id, Students.LastName, Students.FirstName, '
                         'Students.SecondName, Disciplines.Name, Grades.Mark '
                         'FROM Grades, Students, Disciplines '
                         'WHERE Grades.StudentId = Students.Id AND Grades.DisciplineId = Disciplines.Id '
                         'GROUP BY Grades.Id;').fetchall()
    PrintRows(all_rows)

#Выводим таблицу Students
def PrintTableStudents():
     all_rows = c.execute('SELECT Students.Id, Students.LastName, Students.FirstName, Students.SecondName, Groups.Name '
                          'FROM Students, Groups WHERE Students.GroupId = Groups.Id '
                          'GROUP BY Students.Id;').fetchall()
     PrintRows(all_rows)

#1.Подсчет студентов в каждой из групп
def CountStudentsFromGroups():
    all_rows = c.execute('SELECT Groups.Name, COUNT(Students.GroupId) '
                         'FROM Groups , Students WHERE Groups.Id = Students.GroupId '
                         'GROUP BY Groups.Name;').fetchall()
    PrintRows(all_rows)

#2.Средняя оценка каждого студента(если студента нет в таблице, значит у него нет оценок по дисциплинам)
def MiddleMarkStudents():
    all_rows = c.execute('SELECT Students.LastName, Students.FirstName, Students.SecondName, Groups.Name, AVG(Grades.Mark) '
                         'FROM Students, Grades, Groups WHERE Students.Id = Grades.StudentId AND Students.GroupId = Groups.Id '
                         'GROUP BY Students.Id;').fetchall()
    PrintRows(all_rows)

#3.Вывод студентов и их количество имеющих средний балл выше 4.5
def CountStudentsMiddleMarkBiggerThen4_5():
    all_rows = c.execute('SELECT Students.LastName, Students.FirstName, Students.SecondName, Groups.Name, AVG(Grades.Mark) '
                         'FROM Students, Grades, Groups '
                         'WHERE Students.Id = Grades.StudentId AND Students.GroupId = Groups.Id '
                         'GROUP BY Students.Id '
                         'HAVING AVG(Grades.Mark) > 4.5;').fetchall()
    print('Count Students =', len(all_rows))
    PrintRows(all_rows)

#4.Минимальный средний балл студентов наибольший из всех групп
def MinMiddleBall():
    all_rows = c.execute('SELECT Groups.Name, Max(t1.Sred) FROM Groups '
                         'JOIN (SELECT Groups.Name AS gr, MIN(t.Sred) AS Sred FROM Groups '
                         'JOIN (SELECT Groups.Name AS gr, AVG(Grades.Mark) AS Sred FROM Grades,Students,Groups WHERE '
                         'Students.GroupId = Groups.Id AND Students.Id = Grades.StudentId GROUP BY Students.Id) '
                         't ON t.gr = Groups.Name '
                         'GROUP BY Groups.Name) t1 ON t1.gr = Groups.Name;').fetchall()
    PrintRows(all_rows)
#Получение студента с оценкой по матану
def Grade():
    all_rows = c.execute('SELECT Students.LastName, Students.FirstName, Students.SecondName, Groups.Name, Grades.Mark '
                         'FROM Students, Groups, Grades, Disciplines WHERE Students.GroupId = Groups.Id AND Students.Id = Grades.StudentId '
                         'Students.LastName = "Olaf3" AND Disciplines.Name = "Matan" '
                         'Group by Students.LastName;').fetchall()
    PrintRows(all_rows)



def app():
    Grade()
    #CountStudentsFromGroups()
    #MiddleMarkStudents()
    #CountStudentsMiddleMarkBiggerThen4_5()
    #MinMiddleBall()
    #Закрываем соединение с базой
    conn.close()


if __name__ == '__main__':
    app()