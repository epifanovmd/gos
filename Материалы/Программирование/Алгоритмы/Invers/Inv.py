from random import randint
import time

def rnd_mass(n):
    return [randint(0, 10) for i in range(0, n)]

def Poisk(A, B, l, q, r):
    for i in range(l, r):
        if q < i and A[q] > A[i]:
            B.append([A[q], A[i]])


def Invers(A, B, l, q,  r):
    if q > l:
        q -= 1
        Invers(A, B, l, q, r)
        Poisk(A, B, l, q, r)


print("Программа находит инверсии в массиве")
n = int(input("Введите длину массива: "))
A = rnd_mass(n)
B = []
a = time.clock()
Invers(A, B, 0, n, n)
print("Время работы алгоритма =", time.clock() - a)
print("Исходный массив")
print(A)
print("Массив инверсий")
if B.__len__() >= 11:
    for i in range(0, B.__len__() // 11):
        print(B[i*11:(i+1)*11])
else:
    print(B)
print("Число инверсий =", B.__len__())
