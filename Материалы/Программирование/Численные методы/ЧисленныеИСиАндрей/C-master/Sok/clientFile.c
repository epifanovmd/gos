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
    int fd;
    char g;
    printf("File - %s\nIP - %s\n", argv[1], argv[2]);
    int file = open(argv[1], O_RDONLY, 0666);
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

    char* c = malloc(sizeof(char)*1000);  // выделение памяти для отправляемого символа
     // отвечает за  данные
    
    do { // Бесконечный цикл запрос-отправка
        //c = getchar(); // получение символа с клавиатуры
	
	    while (read(file, c, sizeof(char)*1000)!=0)
	{
		write(sockfd, c, sizeof(char)*1000);
		//printf("%c", g);
	} 
	g = getchar();
	
    } while (g != '*'); // символ «*» — конец отправки сообщений

    close(sockfd);
    close(file);
    
    return 0;
}
