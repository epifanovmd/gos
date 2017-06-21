#include <sys/mman.h>
#include <fcntl.h>
#include <unistd.h>
#include <stdio.h>

int main(void) {
  int fd;
  size_t size;
  
  fd = shm_open("/test_shmem",
       O_WRONLY | O_CREAT, 0644);
  if (fd == -1) {
    perror("OPEN");
    return 1;
  }

  double pi = 3.14159265358979323;
  size = write(fd, &pi, sizeof(pi));
  if (size <= 0) {
    perror("WRITE");
    return 1;
  }

  close(fd);
  return 0;
}
