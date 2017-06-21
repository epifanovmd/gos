/// Клиентская часть (без анализа ошибок вызовов)
#include <sys/socket.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>
#include <netinet/in.h>

int main() {
    int sockfd; // Файловый дескриптор, связанный с сокетом

    struct sockaddr_in server;
    server.sin_family = AF_INET;   // сетевой сокет (IPv4)
    server.sin_port = htons(10000);// порт для приёма сообщений
    server.sin_addr.s_addr = inet_addr("127.0.0.1"); // сообщения отправляются на компьютер с адресом 127.0.0.1
    // 127.0.0.1 — всегда компьютер, на котором запущена программа. Вообще, IP может быть абсолютно любым
    // лишь бы пакеты не блокировались файрволлом и на другом конце кто-то их ждал

    // создание сокета
    sockfd = socket(AF_INET,     // сетевой сокет (IPv4)
                    SOCK_STREAM, // двунаправленный надёжный поток данных
                    0            // протокол (0 — IP, Internet Protocol)
                    );

    // соединение с адресом
    connect(sockfd, (struct sockaddr *) &server, sizeof(struct sockaddr_in));

    unsigned char c;  // выделение памяти для отправляемого символа
    do { // Бесконечный цикл запрос-отправка
        c = getchar(); // получение символа с клавиатуры

        write(sockfd, &c, sizeof(unsigned char)); // отправка символа по сети
    } while (c != '*'); // символ «*» — конец отправки сообщений

    close(sockfd);
    
    return 0;
}
