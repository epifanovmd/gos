print("Программа вычисляет суммарное кол-во км за все дни с учётом процента")
perv_den = float(input("Пробег в первый день(км): "))
pr = float(input("Процент от пробега(%): ")) / 100
n = int(input("Количество дней пробега: "))
obshiy = 0
d = 0
while n > 0:
    if d == 0:
        d = perv_den
        obshiy += d
    else:
        d = obshiy*pr
        obshiy += d
    n -= 1
print("Общий пробег = " + str(obshiy) + " км")
