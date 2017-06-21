print("Программа выисляет длину последовательности для заданного m")
m = int(input("Введите m: "))
x0 = 1
x1 = 1
xk = 0
scht = 2
pp = []
pp.append(x0)
pp.append(x1)
while bool(1) and m > 1:
    xk = (x1 + x0) % m
    x0 = x1
    x1 = xk
    pp.append(xk)
    if pp[0] == x0 and pp[1] == x0 and pp[2] == xk and scht != 2:
        scht -= 2
        pp.pop()
        pp.pop()
        pp.pop()
        break
    scht += 1
print(pp)
print("Длина последовательности = " + str(scht))
