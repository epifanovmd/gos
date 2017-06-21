from math import sin, cos

print("Метод Хорд")

def F(x):
    return cos(x) + x - 2

def horda(xn, xk, eps):
    xk1 = 0
    while (abs(F(xk)) >= eps):
        xk1 = xk - F(xk)*((xk - xn)/(F(xk) - F(xn)))
        xk = xk1
    return xk

xn = float(input("Введите x0: ").replace(',', '.'))
xk = float(input("Введите x1: ").replace(',', '.'))
eps = float(input("Введите eps: ").replace(',', '.'))
print("Корень = " + str(horda(xn, xk, eps)))