from math import sqrt

print("Программа решает уравнение вида a*x^2 + b*x + c = 0")
a = float(input("a = "))
b = float(input("b = "))
c = float(input("c = "))
desc = 0
if a == 0:
    print("x =", c/b)
else:
    desc = b**2 - 4*a*c
    if desc == 0:
        print("x1 = x2 =", -b/(2*a))
    elif desc > 0:
        print("x1 =", (-b + sqrt(desc))/(2*a))
        print("x2 =", (-b - sqrt(desc))/(2*a))
    else:
        print("x1 =", complex(-b/(2*a), sqrt(-desc))/(2*a))
        print("x2 =", complex(-b/(2*a), -sqrt(-desc))/(2*a))