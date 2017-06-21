#include <glob.h>
#include <stdio.h>

int main(void) {
  glob_t globbuf;
  
  glob("./*.c", 0, NULL, &globbuf);

  for (int i = 0; i < globbuf.gl_pathc; i++)
    printf("%s\n", globbuf.gl_pathv[i]);

  globfree(&globbuf);
  
  return 0;
}
