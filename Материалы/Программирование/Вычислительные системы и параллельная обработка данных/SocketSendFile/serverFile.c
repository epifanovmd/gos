/// Серверная часть
#include <sys/socket.h>
#include <stdlib.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <stdlib.h>

int main() {
    int sockfd; // Файловый дескриптор, связанный с сокетом
    int blocks = 1;
    struct sockaddr_in server;
    server.sin_family = AF_INET;         // сетевой сокет (IPv4)
    server.sin_port = htons(10000);      // порт для приёма сообщений
    int file = open("receive.txt", O_WRONLY | O_CREAT, 0666);
    server.sin_addr.s_addr = INADDR_ANY; // сообщения принимаются от любого компьютера

    // создание сокета
    sockfd = socket(AF_INET,     // сетевой сокет (IPv4)
                    SOCK_STREAM, // двунаправленный надёжный поток данных
                    0            // код протокола (0 — IP, Internet Protocol)
                    );

    // связывание сокета с адресом
    bind(sockfd,                        // какой сокет связывается             
         (struct sockaddr *) &server,   // приведение адреса к обобщённому виду
         sizeof(struct sockaddr_in)     // размер структуры с адресом
         );

    // включается приём соединений
    listen(sockfd,  // сокет
           8        // длина очереди ожидающих соединений
           );
    
    for(;;) { // бесконечный цикл обработки запросов
        // принимается запрос на соединение
        int conn = accept(sockfd, NULL, NULL);

        char* c = malloc(sizeof(char)*blocks); // выделение памяти для принимаемого символа
        for (;;) {
            // получение следующего символа (l — количество принятых байт)
            int l = read(conn, c, sizeof(char)*blocks);
	    write(file, c, sizeof(char)*blocks);
            if (l == 0) // если поток символов закончился
                break; // выходим и ждём следующее соединение
        }
        printf("%s", "Файл успешно принят\n");
	
    }
}
