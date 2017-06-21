#include <unistd.h>
#include <stdlib.h>
#include <sys/wait.h>
#include <stdio.h>
#include <string.h>

#define BUFSIZE 100

int main(char* argc, char** argv){
  
  char* strin =malloc(BUFSIZE);
  char* str = malloc(BUFSIZE);
  char* cmd = malloc(BUFSIZE);
  
  while(1){
    printf(">");
    fgets(strin, BUFSIZE, stdin);
    printf("{%s}", str);
    strcpy(str, strin);
    str[strlen(str)-1] = '\0';
    cmd = strsep(&str, " ");
      
    if(strcmp (cmd, "exit")==0)
      break;
    pid_t pid = fork();

    if(pid == 0){
      execlp(cmd,cmd,NULL);
    } else {
      int status;
      wait(&status);
    }
  }
  return 0;
}
