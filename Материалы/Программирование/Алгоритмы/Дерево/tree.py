#Узел дерева
class Node:
    def __init__(self, value):
        #Ссылки на левое и правое поддеревья
        self.left = None
        self.right = None
        #Хранимое в узле значение
        self.value = value

class Tree:
    __lst_elem = list()  #содержит список элементов после обхода дерева

    def __init__(self):
         self.__root = None #Ссылка на корень дерева

    def __except_value(self, x):    #Определяем число в дерево добавляем или нет
        try:
            if float.is_integer(float(x.replace(',','.'))):
                x = int(x)
            else:
                x = float(x.replace(',','.'))
        except ValueError:
            return "Not"
        return "Yes"
    #Добавляет в дерево сортировки значение
    def add(self, x):
        if self.__except_value(x) == "Not":
            return print("Not a Number")
        if self.__root is None:
            self.__root = Node(x)
        else:
            current = self.__root #Ссылка на текущий узел
            while True:
                if x > current.value:
                    if current.right is None:
                        current.right = Node(x)
                        break
                    current = current.right
                else:
                    if current.left is None:
                        current.left = Node(x)
                        break
                    current = current.left

    def max(self):
        if self.__root is None:
            return None
        current = self.__root
        while current.right is not None:
            current = current.right
        return current.value

    def min(self):
        if self.__root is None:
            return None
        current = self.__root
        while current.left is not None:
            current = current.left
        return current.value

    def sdvig(self, str):  #Входная строка для доступа к узлу имеет вид: rl(right.left)
        current = self.__root
        for i in range(0, len(str)):
            if str[i] == 'r':
                current = current.right
            elif str[i] == 'l':
                current = current.left
            else:
                return print("Error Input String!")
        if current is None:
            return "None"
        return current.value

    def find_element(self, num):   #Поиск элемента в дереве с выводом позиции в дереве
        if self.__except_value(num) == "Not":
            return print("Not a Number")
        current = self.__root
        k = ''
        while True:
            if current == None:
                return ["Not", None]
            elif num == current.value:
                return ["Yes", k[:len(k)-1]]
            elif num > current.value:
                k += "right."
                current = current.right
            else:
                k += "left."
                current = current.left

    def __obhod_tree_LKP(self, cur):  #Метод обхода (Центрированный) (обычная сортировка по возрастанию)
        if cur is None:
            return list()
        self.__obhod_tree_LKP(cur.left)
        self.__lst_elem.append(cur.value)
        self.__obhod_tree_LKP(cur.right)


    def obhod_tree_LKP(self):   #Метод для вызова метода обхода (Центрированный) (обычная сортировка по возрастанию)
        self.__lst_elem = list()
        self.__obhod_tree_LKP(self.__root)
        return self.__lst_elem

    def __obhod_tree_KLP(self, cur):    #Метод Прямого порядка обхода
        if cur is None:
            return list()
        self.__lst_elem.append(cur.value)
        self.__obhod_tree_KLP(cur.left)
        self.__obhod_tree_KLP(cur.right)

    def obhod_tree_KLP(self):    #Метод для вызова метода Прямого порядка
        self.__lst_elem = list()
        self.__obhod_tree_KLP(self.__root)
        return self.__lst_elem

    def __obhod_tree_LPK(self, cur):    #Метод Обратного порядка
        if cur is None:
            return list()
        self.__obhod_tree_LPK(cur.left)
        self.__obhod_tree_LPK(cur.right)
        self.__lst_elem.append(cur.value)

    def obhod_tree_LPK(self):    #Метод для вызова метода Обратного порядка
        self.__lst_elem = list()
        self.__obhod_tree_LPK(self.__root)
        return self.__lst_elem


    def Zamena_str(self, ss):   #Замена вида rlr на right.left.right
        k = ''
        for i in range(0, len(ss)):
            if ss[i] == 'r':
                k += 'right.'
            elif ss[i] == 'l':
                k += 'left.'
            else:
                return "Error Input String!"
        return k[:len(k)-1]



tree = Tree()
print("Введите элемент(конец вода-None):")
while True:
    k = input('> ')
    if k == 'None':
        break
    else:
        tree.add(k)
p = input('Какой элемент ищется = ')
find = 'rl'
print("Элемент {1} находится в позиции {0}".format(tree.Zamena_str(find), tree.sdvig(find)))
print("Максимальный элемент в дереве = {0}".format(tree.max()))
print("Минимальный элемент в дереве = {0}".format(tree.min()))
rez = tree.find_element(p)
print("Элемент {0}, {1} in Tree! Позиция = {2}".format(p, rez[0], rez[1]))
print("Обход дерева LKP:\n{0}".format(tree.obhod_tree_LKP()))
print("Обход дерева LPK:\n{0}".format(tree.obhod_tree_LPK()))
print("Обход дерева KLP:\n{0}".format(tree.obhod_tree_KLP()))
