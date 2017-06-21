#include <stdio.h>
#include <glob.h>
#include <unistd.h>
#include <sys/stat.h>
#include <time.h>

int main(int argc, char** argv) {
  long allsize = 0;
  glob_t globbuf;
  struct stat info;
   int error = glob("./*.txt", 0, NULL, &globbuf);
   if(error == GLOB_NOMATCH)
     {
       perror("NOMATCH");
     }
   else{
  for (int i = 0; i < globbuf.gl_pathc; i++){
    printf("%s      ", globbuf.gl_pathv[i]);
    stat(globbuf.gl_pathv[i], &info);
    allsize +=info.st_size;
    printf("Size: %ld\n", info.st_size);
  }
   }
    printf("All size = %ld", allsize);
  globfree(&globbuf);

  return 0;
}
