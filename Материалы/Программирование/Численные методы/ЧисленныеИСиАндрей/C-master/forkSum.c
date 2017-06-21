#include <unistd.h>
#include <stdio.h>
#include <math.h>
int main(char* argc, char** argv){
  int fd[2];

  const int N=1000000;
  double a=0, b=1;

  pipe(fd);

  double sum = 0;
  pid_t pid = fork();
  if(pid == 0){
    for(int j = 0; j < N/2; j++)
      sum += exp(a + (b-a)/N *j);
    write(fd[1], &sum, sizeof(double));
  }else{
    for(int j = N/2;j<N;j++){
      sum+= exp(a+((b-a)/N)*j); 
    }
    double sum1;
    read(fd[0], &sum1, sizeof(double));
    sum += sum1;
    sum /=N; 
    printf("Sum =  %lf\n", sum);
  }
  return 0;
}
