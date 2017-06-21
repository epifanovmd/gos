/// Серверная часть
#include <sys/socket.h>
#include <unistd.h>
#include <stdio.h>
#include <string.h>
#include <netinet/in.h>
#include <stdlib.h>

int main() {
    int sockfd; // Файловый дескриптор, связанный с сокетом

    struct sockaddr_in server;
    server.sin_family = AF_INET;           // сетевой сокет (IPv4)
    server.sin_port = htons(8080);         // порт для приёма сообщений
    server.sin_addr.s_addr = (INADDR_ANY); // сообщения принимаются от любого компьютера

    // создание сокета
    sockfd = socket(AF_INET,     // сетевой сокет (IPv4)
                    SOCK_STREAM, // двунаправленный надёжный поток данных
                    0            // код протокола (0 — IP, Internet Protocol)
                    );

    printf("Создан сокет (%d)\n", sockfd);    
    // связывание сокета с адресом
    int err = bind(sockfd,                        // какой сокет связывается             
         (struct sockaddr *) &server,   // приведение адреса к обобщённому виду
         sizeof(server)     // размер структуры с адресом
         );
    printf("Привязан к адресу (%d)\n", err);
    if (err != 0) {
      perror("ERROR:");
      exit(0);
    }
    
    // включается приём соединений
    err = listen(sockfd,  // сокет
           8        // длина очереди ожидающих соединений
           );
    printf("Слушаем порт 8080 (%d)\n", err);
    for(;;) { // бесконечный цикл обработки запросов
        // принимается запрос на соединение
        int conn = accept(sockfd, NULL, NULL);

        printf("Принято соединение\n");

        unsigned char c; // выделение памяти для принимаемого символа
	int sr=0, sn=0;
        for (;;) {
            // получение следующего символа (l — количество принятых байт)
            int l = read(conn, &c, sizeof(unsigned char));

	    // Коряво!
	    if (c == '\r') sr++;
	    else if (c == '\n') sn++;
	    else sr = sn = 0;
	    
            if (l == 0 || (sr == 2 && sn == 2)) // если поток символов закончился
                break; // выходим и ждём следующее соединение

            putchar(c);
        }

	const char * response_header =
	  "HTTP/1.1 200 OK\r\n"
	  "Server: Tiny Brave Server\r\n"
	  "Content-Type: text/plain; charset=UTF-8\r\n" // MIME
	  "\r\n"
	  ;

	const char * response_body = "Hello, world!";

	write(conn, response_header, strlen(response_header));
	write(conn, response_body, strlen(response_body));
		
	close(conn);
        printf("===\n");
    }
}
