from random import randint
from time import time


def rnd_mass(n):
    return [randint(0,10) for i in range(0, n)]

def merge(a, lb, split, ub):
        cur_jmp1 = 0
        pos1 = lb
        pos2 = split+1
        pos3 = 0
        temp = [i for i in range(0, ub-lb+1)]
        while pos1 <= split and pos2 <= ub:
            if a[pos1] < a[pos2]:
                temp[pos3] = a[pos1]
                pos1 += 1
                pos3 += 1
            else:
                temp[pos3] = a[pos2]
                pos2 += 1
                pos3 += 1
                cur_jmp1 += split - pos1 + 1

        while pos2 <= ub:
            temp[pos3] = a[pos2]
            pos3 += 1
            pos2 += 1
        while pos1 <= split:
            temp[pos3] = a[pos1]
            pos3 += 1
            pos1 += 1
        a[lb:ub+1] = temp
        del(temp)
        return cur_jmp1


def  mergeSort(a, lb, ub):
    cur_jmp = 0
    if lb == ub:
        return cur_jmp
    split = (lb + ub) >> 1
    cur_jmp += mergeSort(a, lb, split)
    cur_jmp += mergeSort(a, split+1, ub)
    cur_jmp += merge(a, lb, split, ub)

print("Сортировка массива методом слияний")
n = int(input("Введите кол-во элементов: "))
A = rnd_mass(n)
B = [A[i] for i in range(0, n)]
a = time()
k = mergeSort(A, 0, n - 1)
print("Время алгоритма =", time() - a)
print("Исходный массив")
print(B)
print("Количество инверсий =", k)
