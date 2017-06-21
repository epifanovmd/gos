from math import sqrt

print("Программа вычисляет расстояние м/у двумя точками")
print("Введите координаты первой точки")
x1 = float(input("x1 = "))
y1 = float(input("y1 = "))
print("Введите координаты второй точки")
x2 = float(input("x2 = "))
y2 = float(input("y2 = "))
print("Расстояние м/у точками = ", sqrt((x2-x1)**2 + (y2-y1)**2))

