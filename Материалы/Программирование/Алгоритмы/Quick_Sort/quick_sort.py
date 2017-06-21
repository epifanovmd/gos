import random
import time
from random import randint

def rand_mass(n):
    return [randint(0, 10) for i in range(0, n)]

def QuickSort1(A, l, r):
    if l >= r:
        return
    else:
        q = random.choice(A[l:r + 1])
        i = l
        j = r
        while i <= j:
            while A[i] < q:
                i += 1
            while A[j] > q:
                j -= 1
            if i <= j:
                A[i], A[j] = A[j], A[i]
                i += 1
                j -= 1
    QuickSort1(A, l, j)
    QuickSort1(A, i, r)



def QuickSort2(A):  #Второй метод
    if len(A) <= 1:
        return A
    q = random.choice(A)
    L = [elem for elem in A if elem < q]
    M = [q] * A.count(q)
    R = [elem for elem in A if elem > q]
    return QuickSort2(L) + M + QuickSort2(R)



def QuickSort3(A): #Третий метод(тоже что и 2 только расписанный)
    if len(A) <= 1:
        return A
    q = random.choice(A)
    L, M, R = [],[],[]
    for elem in A:
        if elem < q:
            L.append(elem)
        elif elem > q:
            R.append(elem)
        else:
            M.append(elem)
    return QuickSort3(L) + M + QuickSort3(R)



def QuickSort(A):  #Первый метод(работает медленнее чем QuickSort2)
    QuickSort1(A, 0, len(A) - 1)
    return A


def main():
    print("Сортировка массива методом быстрой сортировки")
    n = int(input("Введите кол-во элементов: "))
    A = rand_mass(n)
    B = A.copy()
    a = time.clock()
    QuickSort(A)
    A = B.copy()
    print("Время алгоритма QuickSort = {0} sec".format(time.clock() - a))
    a = time.clock()
    A = QuickSort2(A)
    print("Время алгоритма QuickSort2 = {0} sec".format(time.clock() - a))
    print("Исходный массив")
    print(B)
    print("Отсортированный массив")
    print(A)

if __name__ == "__main__":
    main()