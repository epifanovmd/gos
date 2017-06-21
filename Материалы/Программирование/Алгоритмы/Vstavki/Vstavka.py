from random import randint
import time

def rna(p):
    return [randint(0, 10) for j in range(0, p)]

def sort(z, A, B):
    for j in range(1, z):
        key = A[j]
        i = j - 1
        while i >= 0 and A[i] > key:
            A[i+1] = A[i]
            i -= 1
            A[i+1] = key
    return A, B

print("Сортирует элементы массива методом вставки")
n = int(input("Введите кол-во элементов: "))
A = rna(n)
B = A.copy()
a = time.clock()
x = sort(n, A, B)
print("Время алгоритма = {0} sec".format(time.clock() - a))
print("Исходный массив", x[1])
print("Отсортированный массив", x[0])





