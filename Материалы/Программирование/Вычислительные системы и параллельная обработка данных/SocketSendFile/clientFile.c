/// Клиентская часть (без анализа ошибок вызовов)
#include <sys/socket.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <stdlib.h>

int main(int argc, char** argv) {
    int sockfd; // Файловый дескриптор, связанный с сокетом
    int blocks = 1;
    printf("File - %s\nIP - %s\n", argv[1], argv[2]);
    int file = open(argv[1], O_RDONLY, O_);
    struct sockaddr_in server;
    server.sin_family = AF_INET;   // сетевой сокет (IPv4)
    server.sin_port = htons(10000);// порт для приёма сообщений
    server.sin_addr.s_addr = inet_addr(argv[2]); // сообщения отправляются на компьютер с адресом 127.0.0.1
    // 127.0.0.1 — всегда компьютер, на котором запущена программа. Вообще, IP может быть абсолютно любым
    // лишь бы пакеты не блокировались файрволлом и на другом конце кто-то их ждал

    // создание сокета

    sockfd = socket(AF_INET,     // сетевой сокет (IPv4)
                    SOCK_STREAM, // двунаправленный надёжный поток данных
                    0            // протокол (0 — IP, Internet Protocol)
                    );

    // соединение с адресом
    connect(sockfd, (struct sockaddr *) &server, sizeof(struct sockaddr_in));

    char* c = malloc(sizeof(char)*blocks);  // выделение памяти для отправляемого символа
     // отвечает за  данные
    
    do { // Бесконечный цикл запрос-отправка
   
	
	    while (read(file, c, sizeof(char)*blocks)!=0)
	{
		write(sockfd, c, sizeof(char)*blocks);
	
	} 
	c = 0;
	
    } while (c != 0);
    printf("%s", "Файл успешно отправлен\n");
    close(sockfd);
    close(file);
    
    return 0;
}
