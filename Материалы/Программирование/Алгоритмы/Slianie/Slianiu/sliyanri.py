from random import randint
import time

def rand_mass(n):
    return [randint(0, 10) for i in range(0, n)]

def merge(a, lb, split, ub):
        pos1 = lb
        pos2 = split+1
        pos3 = 0
        temp = [i for i in range(0, ub-lb+1)]
        while pos1 <= split and pos2 <= ub:
            if a[pos1] > a[pos2]:
                temp[pos3] = a[pos1]
                pos1 += 1
                pos3 += 1
            else:
                temp[pos3] = a[pos2]
                pos2 += 1
                pos3 += 1
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

def mergeSort(a, lb, ub):
    if lb < ub:
        split = int((lb + ub)/2)
        mergeSort(a, lb, split)
        mergeSort(a, split+1, ub)
        merge(a, lb, split, ub)


def main():
    print("Сортировка массива методом слияний")
    n = int(input("Введите кол-во элементов: "))
    A = rand_mass(n)
    B = A.copy()
    a = time.clock()
    mergeSort(A, 0, n - 1)
    print("Время алгоритма = {0} sec".format(time.clock() - a))
    print("Исходный массив")
    print(B)
    print("Отсортированный массив")
    print(A)

if __name__ == "__main__":
    main()



