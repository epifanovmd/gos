print("Программа находит все натуральные корни a^2 + b^2 = c^2 {1,..,10}")
for a in range(1, 11):
    for b in range(1, 11):
        for c in range(1, 11):
            if a**2 + b**2 == c**2:
                print(str(a) + "^2 + " + str(b) + "^2 = " + str(c) + "^2")

