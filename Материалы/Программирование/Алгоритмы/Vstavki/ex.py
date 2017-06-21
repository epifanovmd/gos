lst = list()
for i in range(0, 10):
    lst.append(i)
print(lst)
lst1 = list()
for i in range(10, 20):
    lst1.append(i)
print(lst1)

lst.insert(0, lst1[0])
print(lst)