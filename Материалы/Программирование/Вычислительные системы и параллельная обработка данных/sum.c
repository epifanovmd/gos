#include <unistd.h>
#include <stdio.h>
#include <sys/stat.h>
#include <sys/wait.h>
#include <math.h>
#include <fcntl.h>

int main(int argc, char** argv) {
  mkfifo("/tmp/sum.fifo", 0666);

  int N;
  double a, b;
  sscanf(argv[1], "%d", &N);
  sscanf(argv[2], "%lf", &a);
  sscanf(argv[3], "%lf", &b);
  
  int pid = fork();
  if (pid != 0) { // Потомок
    double sum = 0.0;
    for (int k = 0; k < N / 2; k++) 
      sum += cos(a + (b-a)/N * k);

    int fd = open("/tmp/sum.fifo", O_WRONLY);
    write(fd, &sum, sizeof(sum));
    close(fd);
    return 0;
  } else {
    double sum = 0.0;
    for (int k = N/2; k < N; k++) 
      sum += cos(a + (b-a)/N * k);

    int status;
    wait(&status);

    double sum2;
    int fd = open("/tmp/sum.fifo", O_RDONLY);
    read(fd, &sum2, sizeof(sum2));
    close(fd);

    printf("S = %lf\n", (b-a)/N*(sum + sum2));
  }
  return 0;
}
