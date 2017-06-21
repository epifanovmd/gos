import random
import copy

class root:
    def __init__(self, value = None, next = None):
        self.value = value
        self.next = next


class MyList:
    def __init__(self):
        self.this = None
        self.last = None
        self.length = 0

    def __str__(self):
        if self.this is not None:
            cur = self.this
            prnt = "[{0}".format(cur.value)
            while cur.next is not None:
                cur = cur.next
                prnt += ", {0}".format(cur.value)
            return prnt + ']'
        return "[]"

    def __len__(self):
        return self.length

    def clear(self):
        self.__init__()

    def Push(self, x):
        self.length += 1
        if self.this is None:
            self.this = self.last = root(x, None)
        else:
            self.this = root(x, self.this)

    def add(self, x):
        self.length += 1
        if self.this is None:
            self.this = self.last = root(x, None)
        else:
            self.last.next = self.last = root(x, None)


    def insert(self, ind, x):
        self.length += 1
        if self.this is None:
            self.this = root(x, self.this)
            self.last = self.this.next
            return
        if ind == 0:
            self.this = root(x, self.this)
            return
        cur = self.this
        cnt = 0
        while cur is not None:
            cnt += 1
            if cnt == ind:
                cur.next = root(x, cur.next)
                if cur.next.next is None:
                    self.last = cur.next
                break
            cur = cur.next



def main():
    lst = MyList()
    lst.Push(1)
    lst.Push(2)
    lst.Push(3)
    print("Push - %s" %lst)
    print("Length = %s" %len(lst))
    lst.clear()
    lst.add(1)
    lst.add(2)
    lst.add(3)
    print("Add - %s" %lst)
    print("Length = %s" %len(lst))
    lst.clear()
    lst.add(1)
    lst.add(2)
    lst.add(3)
    lst.insert(2, 25)
    print("Insert on index 2 element 25 - %s" %lst)
    print("Length = %s" %len(lst))
    lst1 = copy.deepcopy(lst)
    print("Copy lst - %s" %lst1)

if __name__ == "__main__":
    main()