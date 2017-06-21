
def FindElem(a, l, r):
    return [str(a[i]) + '=' + str(i) for i in range(l, r) if a[i] == i]

def FindRek(a, l, r, res = []):
    if l < r:
        m = int((l + r)/2)
        FindRek(a, l, m)
        FindRek(a, m+1, r)
        return FindElem(a, l, r)

def FindElemEqualIndex(A):
    return FindRek(A, 0, len(A))

A = [0,2,2,8,4,5]
print('Nachalni massiv = {0}'.format(A))
res = FindElemEqualIndex(A)
print('Result = {0}'.format(res))