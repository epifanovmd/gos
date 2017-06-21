print("Алгоритм Евклида (НОД)")


def Evklid(a, b):
    if b == 0:
        return a
    c = b
    b = a % b
    a = c
    return Evklid(a, b)



a = int(input("Введите первое число: "))
b = int(input("Введите второе число: "))
if b > a:
    a, b = b, a
print("НОД(", a, ",", b,") =", Evklid(a, b))