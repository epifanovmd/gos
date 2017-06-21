#include <sys/mman.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/stat.h>
#include <stdio.h>

int main(void) {
  int fd, ret;
  size_t size;
  const char* name = "/test_shmem";
  fd = shm_open(name, O_RDONLY, 0644);
  if (fd == -1) {
    perror("OPEN");
    return 1;
  }

  double pi;
  size = read(fd, &pi, sizeof(pi));
  if (size <= 0) {
    perror("READ");
    return 1;
  }
    
  printf("PI = %lf\n", pi);
  close(fd);

  ret = shm_unlink(name);
  if (ret == -1) {
    perror("UNLINK");
    return 1;
  }

  return 0;
}
