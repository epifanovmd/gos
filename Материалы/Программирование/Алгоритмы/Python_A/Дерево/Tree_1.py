
class Node:
    def __init__(self, value):
        self.left = None
        self.right = None
        self.value = value

class Tree:
    __lst_element = list()

    def __init__(self):
         self.__root = None

    def __except_value(self, x):
        try:
            if float.is_integer(float(x.replace(',','.'))):
                x = int(x)
            else:
                x = float(x.replace(',','.'))
        except ValueError:
            return "Not"
        return "Yes"

    def add(self, x):
        if self.__except_value(x) == "Not":
            return print("Not a Number")
        if self.__root is None:
            self.__root = Node(x)
        else:
            current = self.__root
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

    def sdvig(self, str):
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

    def poisk_element(self, num):
        if self.__except_value(num) == "Not":
            return print("Not a Number")
        current = self.__root
        while True:
            if current.value == None:
                return "Not"
            elif num == current.value:
                return "Yes"
            elif num > current.value:
                current = current.right
            else:
                current = current.left

    def __obhod_tree_LKP(self, cur):
        if cur is not None:
            self.__obhod_tree_LKP(cur.left)
            self.__lst_element.append(cur.value)
            self.__obhod_tree_LKP(cur.right)

    def obhod_tree_LKP(self):
        cur = self.__root
        self.__obhod_tree_LKP(cur)
        lst = self.__lst_element.copy()
        self.__lst_element.clear()
        return lst

    def __obhod_tree_KLP(self, cur):
        if cur is not None:
            self.__lst_element.append(cur.value)
            self.__obhod_tree_KLP(cur.left)
            self.__obhod_tree_KLP(cur.right)

    def obhod_tree_KLP(self):
        cur = self.__root
        self.__obhod_tree_KLP(cur)
        lst = self.__lst_element.copy()
        self.__lst_element.clear()
        return lst

    def __obhod_tree_LPK(self, cur):
        if cur is not None:
            self.__obhod_tree_LPK(cur.left)
            self.__obhod_tree_LPK(cur.right)
            self.__lst_element.append(cur.value)

    def obhod_tree_LPK(self):
        cur = self.__root
        self.__obhod_tree_LPK(cur)
        lst = self.__lst_element.copy()
        self.__lst_element.clear()
        return lst




tree = Tree()
print("Vvedite elements(end-None):")
while True:
    k = input('> ')
    if k == 'None':
        break
    else:
        tree.add(k)
p = input('poisk element = ')


print("Element = {0}".format(tree.sdvig('rl')))
print("Max = {0}".format(tree.max()))
print("Min = {0}".format(tree.min()))
print("Element {0}, {1} in Tree!".format(p, tree.poisk_element(p)))
print("Obhod Tree LKP:\n{0}".format(tree.obhod_tree_LKP()))
print("Obhod Tree LPK:\n{0}".format(tree.obhod_tree_LPK()))
print("Obhod Tree KLP:\n{0}".format(tree.obhod_tree_KLP()))
