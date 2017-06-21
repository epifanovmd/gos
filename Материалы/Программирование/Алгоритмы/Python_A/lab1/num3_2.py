print("Программа переводит байты в читаемый вид")
byte = int(input("Введите байты: "))
byte1 = byte // (1024**3)
byte2 = (byte - byte1) // (1024**2)
byte3 = ((byte - byte1) % (1024**2)) // 1024
byte4 = ((byte - byte1) % (1024**2)) % 1024
print(str(byte) + " байт = " + str(byte1) + " Гб " + str(byte2) +
      " Мб " + str(byte3) + " КилоБ " + str(byte4) + " Байт")
