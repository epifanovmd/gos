#include <stdio.h>
#include <unistd.h>
#include <sys/stat.h>
#include <time.h>

int main(int argc, char** argv) {
  if (argc == 1) {
    printf("USAGE: %s filename\n", argv[0]);
    return 1;
  }

  struct stat info;
  int ret = stat(argv[1], &info);

  if (ret == -1) {
    perror("ERROR");
    return 1;
  }

  printf("UID: %d\n", info.st_uid);
  printf("Size: %ld\n", info.st_size);
  printf("Modification time: %s\n", ctime(&info.st_mtim.tv_sec));

  return 0;
}
