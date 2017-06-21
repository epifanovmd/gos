#include <stdio.h>
#include <unistd.h>
#include <fcntl.h>
#include <stdlib.h>
#include <sys/socket.h>
#include <sys/un.h>
#include <sys/stat.h>
#include <errno.h>

int main(char* argc, char** argv){

  const char * path = "foo.fifo"; 
  mkfifo(path, 0600);

  double sum1;

  int f = open(path, O_RDONLY);
  read(f, &sum1, sizeof(double));
  printf("%lf\n", sum1);
  close(f);  
  return 0;
}
