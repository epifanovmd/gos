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
  int f = open(path, O_WRONLY);
  double sum1 = 3.14;
    
  write(f, &sum1, sizeof(double));
  close(f);
    
  return 0;
}
